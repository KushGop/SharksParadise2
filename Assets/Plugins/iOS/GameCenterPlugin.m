//
//  GameCenterPlugin.m
//  Unity-iPhone
//
//  Created by Kush Gopeechund on 2025-01-29.
//

#import <Foundation/Foundation.h>
#import <GameKit/GameKit.h>

// This is the method we'll call from Unity to get the player info.
void _getPlayerDisplayName() {
    // Ensure the Game Center local player is authenticated
    GKLocalPlayer *localPlayer = [GKLocalPlayer localPlayer];
    if (localPlayer.isAuthenticated) {
        // Log the display name of the local player
        NSLog(@"Player Display Name: %@", localPlayer.displayName);
        // You can send this info back to Unity if needed (e.g., via UnitySendMessage)
        // UnitySendMessage("YourGameObjectName", "OnPlayerDataReceived", [localPlayer.displayName UTF8String]);
    } else {
        NSLog(@"Player is not authenticated with Game Center.");
    }
}

void _getPlayerNameFromUserID(const char* userID) {
    NSString *playerID = [NSString stringWithUTF8String:userID];
    
    // Load player data for the given userID
    [GKPlayer loadPlayersForIdentifiers:@[playerID] withCompletionHandler:^(NSArray *players, NSError *error) {
        if (error) {
            NSLog(@"Error loading player: %@", error.localizedDescription);
            return;
        }

        if (players.count > 0) {
            GKPlayer *player = players[0];
            NSLog(@"Player Name: %@", player.displayName);
            // You can send this data back to Unity if needed using UnitySendMessage
            UnitySendMessage("PopulateLeaderboard", "OnUsernameReceived", [player.displayName UTF8String]);
        } else {
            NSLog(@"Player not found or invalid userID.");
        }
    }];
}

void _loadLeaderboardScores(const char* leaderboardID, int rowCount, const char* gameObjectName, bool isCentered) {
    NSString *leaderboardIdentifier = [NSString stringWithUTF8String:leaderboardID];
NSString *unityObjectName = [NSString stringWithUTF8String:gameObjectName]; 

    GKLeaderboard *leaderboard = [[GKLeaderboard alloc] init];
    leaderboard.identifier = leaderboardIdentifier;
    leaderboard.timeScope = GKLeaderboardTimeScopeAllTime; // or GKLeaderboardTimeScopeToday for daily

    leaderboard.range = NSMakeRange(0,rowCount); // This limits the number of scores we fetch

    // Load the scores
    [GKLeaderboard loadLeaderboardsWithIDs:@[leaderboardIdentifier] completionHandler:^(NSArray<GKLeaderboard *> *leaderboards, NSError *error) {
    GKLeaderboard *leaderboard = leaderboards.firstObject;
    [leaderboard loadEntriesForPlayerScope:GKLeaderboardPlayerScopeGlobal timeScope:GKLeaderboardTimeScopeAllTime range:NSMakeRange(0, rowCount) completionHandler:^(GKLeaderboardEntry *localPlayerEntry, NSArray<GKLeaderboardEntry *> *entries, NSInteger totalPlayerCount, NSError *error) {
        if (error) {
            NSLog(@"Error loading leaderboard: %@", error.localizedDescription);
            UnitySendMessage("GameCenterManager", "OnLeaderboardLoaded", "Error loading leaderboard");
            return;
        }

        NSMutableString *resultString = [NSMutableString string];
        for (GKLeaderboardEntry *entry in entries) {
            NSString *scoreData = [NSString stringWithFormat:@"%ld,%@,%lld;", (long)entry.rank, entry.player.displayName, entry.score];
            [resultString appendString:scoreData];
        }
if (resultString.length == 0) {
    UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", "No data available");
} else {
    UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", [resultString UTF8String]);
}
        //UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", [resultString UTF8String]);
    }];
}];
}
