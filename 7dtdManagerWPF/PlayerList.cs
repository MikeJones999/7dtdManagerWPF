using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7dtdManagerWPF
{
    class PlayerList
    {
        private List<Player> playerList;

        public PlayerList()
        {
            playerList = new List<Player>();
        }
      
         public List<Player>  getPlayers()
         {
                return playerList;
         }

        public void addPlayer(Player player)
        {
            playerList.Add(player);
        }

    }
}
