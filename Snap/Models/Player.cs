using System.Collections.Generic;

namespace Snap.Models
{
    public class Player
    {
        public string Name { get; set; }
        public IList<PlayingCard> WonCards { get; set; }
    }
}
