[System.Serializable]
public class PlayerData
{
    public float stopTheChrono;

    public PlayerData(ScSaveZone player)
    {
        stopTheChrono = player.stopTheChrono;
    }
}
