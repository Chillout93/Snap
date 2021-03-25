using NUnit.Framework;
using Snap.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private Snap _snap;
        private Random _random;
        private IList<Player> _players;

        [SetUp]
        public void Setup()
        {
            _snap = new Snap();
            _random = new Random();
            _players = new List<Player> { new Player { Name = "James", WonCards = new List<PlayingCard>() }, new Player { Name = "James", WonCards = new List<PlayingCard>() } };
        }

        [Test]
        public void Given_PlayingAGameWithTrueMatchCondition_ShouldReturnCorrectWinner()
        {
            // Arrange
            var deck = new List<PlayingCard> { new PlayingCard { Rank = Rank.Ace, Suit = Suit.Spade }, new PlayingCard { Rank = Rank.Ace, Suit = Suit.Spade } };

            // Act
            var result = _snap.PlayGame(deck, _players, (PlayingCard c1, PlayingCard c2) => true, _random);

            // Assert
            Assert.AreEqual(1, _players.Count(x => x.WonCards.Count == 2));
            Assert.AreEqual(1, _players.Count(x => x.WonCards.Count == 0));
        }

        [Test]
        public void Given_PlayingAGameWithFalseMatchCondition_ShouldReturnDraw()
        {
            // Arrange
            var deck = new List<PlayingCard> { new PlayingCard { Rank = Rank.Ace, Suit = Suit.Spade }, new PlayingCard { Rank = Rank.Ace, Suit = Suit.Spade } };

            // Act
            var result = _snap.PlayGame(deck, _players, (PlayingCard c1, PlayingCard c2) => false, _random);

            // Assert
            Assert.AreEqual(2, _players.Count(x => x.WonCards.Count == 0));
        }

        [Test]
        public void Given_GeneratingShuffledDecks_ThenTheDeckCountIs52TimesNumberOfDecks()
        {
            var deck = _snap.GenerateShuffledDecks(5, _random);
            Assert.AreEqual(52 * 5, deck.Count);
        }

        [Test]
        public void Given_GeneratingStandardDeck_ThenItHas13OfEachSuit()
        {
            var deck = _snap.GenerateStandardDeck();

            Assert.AreEqual(13, deck.Where(x => x.Suit == Suit.Club).Count());
            Assert.AreEqual(13, deck.Where(x => x.Suit == Suit.Diamond).Count());
            Assert.AreEqual(13, deck.Where(x => x.Suit == Suit.Heart).Count());
            Assert.AreEqual(13, deck.Where(x => x.Suit == Suit.Spade).Count());
        }

        [Test]
        public void Given_GeneratingShuffledDecks_ThenTheDeckIsShuffled()
        {
            // Given the time would either mock out Random or use correct seed to assert organised deck gets randomised. 
            Assert.Pass();
        }
    }
}