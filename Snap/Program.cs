using System;
using System.Linq;
using System.Collections.Generic;
using Snap.Models;

namespace Snap
{
    static class Program
    {
        // We should put this in a data section eventually if going functional route, otherwise could have some kind of service class.
        private static readonly Dictionary<string, Func<PlayingCard, PlayingCard, bool>> _matchConditions = new Dictionary<string, Func<PlayingCard, PlayingCard, bool>>
        {
            {  "SUIT", (PlayingCard p1, PlayingCard p2) => p1.Suit == p2.Suit },
            {  "VALUE", (PlayingCard p1, PlayingCard p2) => p1.Rank == p2.Rank },
            {  "SUITANDVALUE", (PlayingCard p1, PlayingCard p2) => p1.Rank == p2.Rank && p1.Suit == p2.Suit }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Snap!");
            var deckCount = GetDeckCount();
            var matchCondition = GetMatchCondition(_matchConditions);

            var random = new Random();
            var snapGame = new Snap();

            while (true)
            {
                var players = new List<Player> { new Player { Name = "James", WonCards = new List<PlayingCard>() }, new Player { Name = "Jay", WonCards = new List<PlayingCard>() } };
                var deck = snapGame.GenerateShuffledDecks(deckCount, random);
                var result = snapGame.PlayGame(deck, players, matchCondition, random);
                Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => $"PLAYER: {x.Name}, COUNT: {x.WonCards.Count}")));
                Console.WriteLine(EvaluateWinner(result));
                Console.ReadLine();
            }
        }

        private static int GetDeckCount()
        {
            var decks = 0;
            Console.WriteLine("How many decks to play with? (1-100)");
            while (decks <= 0 || decks > 100)
            {
                Console.Write("Answer: ");
                var parseSuccess = int.TryParse(Console.ReadLine().ToUpper(), out decks);
                if (!parseSuccess) Console.WriteLine("Please enter a number between 1 and 100");
            }

            return decks;
        }

        private static string EvaluateWinner(IList<Player> players)
        {
            var winner = players.OrderByDescending(x => x.WonCards.Count).FirstOrDefault();
            var drawPlayers = players.Where(x => x.WonCards.Count == winner.WonCards.Count);
            return drawPlayers.Count() > 1 
                ? $"We have a draw between the following players: {string.Join(", ", drawPlayers.Select(x => x.Name))}" 
                : $"{winner.Name} is the winner!";
        }

        private static Func<PlayingCard, PlayingCard, bool> GetMatchCondition(Dictionary<string, Func<PlayingCard, PlayingCard, bool>> matchConditions)
        {
            Func<PlayingCard, PlayingCard, bool> matchCondition = null;

            Console.WriteLine("Which match condition do you want to use? (SUIT,VALUE,SUITANDVALUE)");
            Console.WriteLine("SUIT: (Cards will match based on the Suit e.g. Club, Diamond, Spade or Heart");
            Console.WriteLine("VALUE: (Cards will match based on the Value e.g. Ace, Two, Jack etc.");
            Console.WriteLine("SUITANDVALUE: (Cards will match based on the Suit and Value.");

            while (matchCondition == null)
            {
                Console.Write("Answer: ");
                var parseSuccess = matchConditions.TryGetValue(Console.ReadLine(), out matchCondition);
                if (!parseSuccess) Console.WriteLine("Please enter a valid condition: SUIT, VALUE or SUITANDVALUE");
            }

            return matchCondition;
        }
    }
}
