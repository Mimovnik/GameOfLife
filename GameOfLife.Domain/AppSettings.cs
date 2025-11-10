namespace GameOfLife.Domain;

public class AppSettings
{
    public string SavePath { get; set; }
    public string DeadCellColor { get; set; }
    public string AliveCellColor { get; set; }
    public string DeadCellCharacter { get; set; }
    public string AliveCellCharacter { get; set; }

    public AppSettings()
    {
        SavePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GameOfLife",
            "Saves"
        );
        DeadCellColor = "#1E1E1E";
        AliveCellColor = "#4CAF50";
        DeadCellCharacter = "";
        AliveCellCharacter = "";
    }

    public AppSettings(string savePath, string deadCellColor, string aliveCellColor, 
                       string deadCellCharacter, string aliveCellCharacter)
    {
        SavePath = savePath;
        DeadCellColor = deadCellColor;
        AliveCellColor = aliveCellColor;
        DeadCellCharacter = deadCellCharacter;
        AliveCellCharacter = aliveCellCharacter;
    }
}
