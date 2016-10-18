using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7dtdManagerWPF
{
    class Player
    {
        private int Id;
        private string name;
        private Position pos; // = (1847.5, 4.1, -270.5), 
        private Rotation rot; // = (-29.5, 59.1, 0.0),
        private bool remote; // = True, 
        private int health; //= 100,
        private int deaths; //= 7,
        private int zombieKills; //= 1177,
        private int playerKills; // = 0,
        private int score; // = 1142,
        private int level; // = 144,
        private long steamid; // = 76561198015252609,
        private string ip; // = 80.89.51.95,
        private int ping; // = 76

        public int id
        {
            get{  return Id; }
            set{  Id = value;}
        }

        public string Name
        {
            get{return name;}
            set{name = value;}
        }

        public bool Remote
        {
            get { return remote; }
            set{remote = value;}
        }

        public int Health
        {
            get{return health;}
            set{health = value;}
        }

        public int Deaths
        {
            get{return deaths;}
            set{deaths = value;}
        }

        public int ZombieKills
        {
            get{return zombieKills;}
            set {zombieKills = value;}
        }

        public int PlayerKills
        {
            get{ return playerKills;}
            set{playerKills = value;}
        }

        public int Score
        {
            get{return score;}
            set{ score = value;}
        }

        public int Level
        {
            get{ return level;}
            set{level = value;}
        }

        public long Steamid
        {
            get{return steamid;}
            set{steamid = value;}
        }

        public string Ip
        {
            get { return ip;}
            set{ip = value;}
        }

        public int Ping
        {
            get{ return ping;}
            set{ping = value;}
        }
        //id = 14588, sfur, pos = (1847.5, 4.1, -270.5), rot = (-29.5, 59.1, 0.0), remote = True, health = 100, deaths = 7, zombies = 1177, players = 0, score = 1142, level = 144, steamid = 76561198015252609, ip = 80.89.51.95, ping = 76



    }
}
