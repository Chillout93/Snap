using Snap.Models;
using Snap.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap
{
    public class Snap
    {
        public IList<PlayingCard> GenerateStandardDeck()
        {
            var suits = new List<Suit> { Suit.Club, Suit.Diamond, Suit.Heart, Suit.Spade };
            var ranks = new List<Rank> { Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King };

            return (from s in suits
                    from r in ranks
                    select new PlayingCard { Suit = s, Rank = r }).ToList();
        }

        public IList<PlayingCard> GenerateShuffledDecks(int numberOfDecks, Random rng)
        {
            var playingCards = new List<PlayingCard>();
            for (int i = 0; i < numberOfDecks; i++) 
                playingCards = playingCards.Concat(GenerateStandardDeck()).ToList();

            playingCards.Shuffle(rng);
            return playingCards;
        }

        public IList<Player> PlayGame(IList<PlayingCard> deck, IList<Player> players, Func<PlayingCard, PlayingCard, bool> matchCondition, Random random)
        {
            var previousCards = new List<PlayingCard>() { deck[0] };
            var previousCard = deck[0]; // Keep a separate reference as the player takes previous card deck occasionally.
            for (var i = 1; i < deck.Count; i++)
            {
                var currentCard = deck[i];
                previousCards.Add(currentCard);
                if (previousCards.Count > 0 && matchCondition(currentCard, previousCard))
                {
                    var player = players[random.Next(0, players.Count)];
                    player.WonCards = player.WonCards.Concat(previousCards).ToList();
                    previousCards = new List<PlayingCard>();
                }
                previousCard = currentCard;
            }

            return players;
        }
    }
}
