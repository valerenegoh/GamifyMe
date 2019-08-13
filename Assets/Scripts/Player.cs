using System;

[Serializable]
public class Player{
    public string name;
    public int lvl1score = 0;
    public int lvl2score = 0;
    public int lvl3score = 0;
    public int lvl4score = 0;
    public int latestLvl = 0;

    public Player(string pname, int lvl1, int lvl2, int lvl3, int lvl4, int levelPassed){
        name = pname;
        lvl1score = lvl1;
        lvl2score = lvl2;
        lvl3score = lvl3;
        lvl4score = lvl4;
        latestLvl = levelPassed;
    }
}