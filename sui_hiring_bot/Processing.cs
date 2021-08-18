using System;
using Discord;
using Discord.WebSocket;

namespace sui_hiring_bot
{
    public class Processing
    {
        public void ProcessControl(SocketMessage message)
        {
            try
            {
                if (DatabaseHandler.ExecuteScalar("SELECT 1 FROM job_table WHERE status='ACTIVE'") == "1")
                {
                    message.Channel.SendMessageAsync("Process already running.");
                    return;
                }
                DatabaseHandler.ExecuteNonQuery("INSERT INTO job_table(status) VALUES('ACTIVE')");
                while (true)
                {
                    var builder = new EmbedBuilder();
                    builder.WithColor(Color.Magenta);
                    builder.WithTitle("Recruitment table - results");

                    var activeResults =
                        DatabaseHandler.ExecuteReaderStringString(
                            "SELECT ship_name, zkillboard_url FROM target_ships WHERE status='ACTIVE' AND tracked=1");
                    foreach (var result in activeResults)
                    {
                        var parsedShipId = result.Item2.Split("/");
                        var activeSearchResults = WebParser.ScanForShips(parsedShipId[4], result.Item1);
                        int ignoredPlayers = 0;
                        int alreadyProcessedPlayers = 0;
                        foreach (var activeSearch in activeSearchResults)
                        {
                            if (activeSearch.Item1 == string.Empty || activeSearch.Item2 == string.Empty || activeSearch.Item3 == string.Empty)
                            {
                                ignoredPlayers++;
                                continue;
                            }
                            if (DatabaseHandler.ExecuteScalar($"SELECT 1 FROM input_players WHERE player_name='{activeSearch.Item1}'") == "1")
                            {
                                alreadyProcessedPlayers++;
                                continue;
                            }
                            DatabaseHandler.ExecuteNonQuery(
                                $"INSERT INTO input_players(player_name, character_id, system_name, player_ship) " +
                                $"VALUES('{activeSearch.Item1}','{activeSearch.Item3}','{activeSearch.Item2}', '{result.Item1}')");
                        }

                        builder.AddField($"{result.Item1}", $"Processed {activeSearchResults.Count-ignoredPlayers-alreadyProcessedPlayers}/50 players\nSkipped {ignoredPlayers} \nAlready in database {alreadyProcessedPlayers}", true);
                    }

                    var potencialCandidates =
                        DatabaseHandler.ExecuteScalar(
                            "SELECT COUNT(player_name) FROM input_players WHERE system_name >= 0.5");
                    builder.WithFooter($"Total ship types scanned: {activeResults.Count}\nPotencial candidates: {potencialCandidates}\nProcessed candidates: 0");
                    message.Channel.SendMessageAsync("", false, builder.Build());
                    if (DatabaseHandler.ExecuteScalar("SELECT 1 FROM job_table WHERE status='ACTIVE'") == "1")
                    {
                        System.Threading.Thread.Sleep(1800000);
                        continue;
                    }
                    break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                message.Channel.SendMessageAsync(e.Message);
                throw;
            }
        }
        public void StopControl(SocketMessage message)
        {
            try
            {
                var insertNewJob = DatabaseHandler.ExecuteNonQuery("UPDATE job_table SET status = 'NONACTIVE'");
                if (insertNewJob)
                {
                    message.Channel.SendMessageAsync("Stopped");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}