[System.Serializable]
public class UserData {
    public string playerName;
    public bool hasGame1;
    public bool hasGame2;
    public bool hasGame3;
    public bool hasGame4;
    public bool hasGame5;
    public bool hasGame6;
    public bool hasGame7;
    public bool hasGame8;
    public bool hasGame9;
    public bool hasGame10;
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;

	public UserData(string username, bool game1, bool game2, bool game3, bool game4, bool game5, bool game6, bool game7,
				 bool game8, bool game9, bool game10, float bgmVol, float sfxVol) {
        playerName = username;
        hasGame1 = game1;
        hasGame2 = game2;
        hasGame3 = game3;
        hasGame4 = game4;
        hasGame5 = game5;
        hasGame6 = game6;
        hasGame7 = game7;
        hasGame8 = game8;
        hasGame9 = game9;
        hasGame10 = game10;
        bgmVolume = bgmVol;
        sfxVolume = sfxVol;
    }

}
