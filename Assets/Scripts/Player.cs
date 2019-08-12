using System;

[Serializable]
public class Player{
    public string name;
    public int lvl1score = 0;
    public int lvl2score = 0;
    public int lvl3score = 0;
    public int lvl4score = 0;
    public int latestLvl = 0;

    public void setName(string pname){
        name = pname;
    }

    public string getName(){
        return name;
    }

    //returns true if highscore
    public bool setLvlscore(int lvl, int score){
        switch(lvl){
            case 1:
                if(score>lvl1score){
                    lvl1score = score;
                    return true;
                }
                return false;
            case 2:
                if(score>lvl2score){
                    lvl2score = score;
                    return true;
                }
                return false;
            case 3:
                if(score>lvl3score){
                    lvl3score = score;
                    return true;
                }
                return false;
            case 4:
                if(score>lvl4score){
                    lvl4score = score;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    public int getLvlscore(int lvl){
        switch(lvl){
            case 1:
                return lvl1score;
            case 2:
                return lvl2score;
            case 3:
                return lvl3score;
            case 4:
                return lvl4score;
            default:
                return 0;
        }
    }


    public void setlatestLvl(int lvl){
        latestLvl = lvl;
    }

    public int getlatestLvl(){
        return latestLvl;
    }
}