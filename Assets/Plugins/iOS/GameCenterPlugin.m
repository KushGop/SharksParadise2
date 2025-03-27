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

void recursiveLoad(GKLeaderboard *leaderboard, NSString *unityObjectName, NSInteger leaderboardStartIndex, NSInteger pageLength,NSMutableArray *top14Players) {
    [leaderboard loadEntriesForPlayerScope:GKLeaderboardPlayerScopeGlobal
                                   timeScope:GKLeaderboardTimeScopeAllTime
                                       range:NSMakeRange(leaderboardStartIndex, leaderboardStartIndex + pageLength)
                          completionHandler:^(GKLeaderboardEntry *localPlayerEntry, NSArray<GKLeaderboardEntry *> *entries, NSInteger totalPlayerCount, NSError *error) {
        if (error) {
            NSLog(@"Error loading leaderboard entries: %@", error.localizedDescription);
            UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", "Error loading leaderboard entries");
            return;
        }
        
        NSMutableString *resultString = [NSMutableString string];
        
        //Store top 14 players incase player is not in leaderboard
        if(leaderboardStartIndex==0){
            for (int i = 0; i < MIN(14, entries.count); i++) {
                [top14Players addObject:entries[i]];
            }
        }
        // If local player's rank is less than 15, just send top 14
        if(localPlayerEntry){
            if (localPlayerEntry.rank < 15) {
                for (int i = 0; i < MIN(14, entries.count); i++) {
                    GKLeaderboardEntry *entry = entries[i];
                    NSString *scoreData = [NSString stringWithFormat:@"%ld,%@,%lld;",
                                           (long)entry.rank,
                                           entry.player.displayName,
                                           entry.score];
                    [resultString appendString:scoreData];
                }
            }else{
                // Get top 3 players
                NSMutableArray *topThreePlayers = [NSMutableArray array];
                for (int i = 0; i < MIN(3, top14Players.count); i++) {
                    [topThreePlayers addObject:top14Players[i]];
                }
                
                // Create a list centered around local player
                NSMutableArray *centeredList = [NSMutableArray array];
                
                // Add top 3 first
                [centeredList addObjectsFromArray:topThreePlayers];
                
                // Calculate start index for local player centered list
                NSInteger startIndex = MAX(0, localPlayerEntry.rank - 5);
                NSInteger endIndex = MIN(entries.count, startIndex + 11);
                
                // Add local player centered entries
                for (NSInteger i = startIndex; i < endIndex; i++) {
                    [centeredList addObject:entries[i]];
                }
                
                // Convert centered list to result string
                [resultString setString:@""];
                for (GKLeaderboardEntry *entry in centeredList) {
                    NSString *scoreData = [NSString stringWithFormat:@"%ld,%@,%lld;",
                                           (long)entry.rank,
                                           entry.player.displayName,
                                           entry.score];
                    [resultString appendString:scoreData];
                }
            }
        }else{
            if(leaderboard.maxRange > leaderboardStartIndex + pageLength){
                recursiveLoad(leaderboard, unityObjectName, leaderboardStartIndex+pageLength, pageLength, top14Players);
            }
        }
        
        // Send results to Unity
        if (resultString.length == 0) {
            UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", "No data available");
        } else {
            UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", [resultString UTF8String]);
        }
    }];
}

void _loadLeaderboardScores(const char* leaderboardID, const char* gameObjectName) {
    NSString *leaderboardIdentifier = [NSString stringWithUTF8String:leaderboardID];
    NSString *unityObjectName = [NSString stringWithUTF8String:gameObjectName]; 
    
    GKLeaderboard *leaderboard = [[GKLeaderboard alloc] init];
    leaderboard.identifier = leaderboardIdentifier;
    leaderboard.timeScope = GKLeaderboardTimeScopeAllTime;
    
    [GKLeaderboard loadLeaderboardsWithIDs:@[leaderboardIdentifier] completionHandler:^(NSArray<GKLeaderboard *> *leaderboards, NSError *error) {
        if (error) {
            NSLog(@"Error loading leaderboards: %@", error.localizedDescription);
            UnitySendMessage([unityObjectName UTF8String], "OnLeaderboardLoaded", "Error loading leaderboard");
            return;
        }
        GKLeaderboard *leaderboard = leaderboards.firstObject;
        NSMutableArray *top14Players = [NSMutableArray array];
        recursiveLoad(leaderboard, unityObjectName, 0, 1000, top14Players);
    }];
}

