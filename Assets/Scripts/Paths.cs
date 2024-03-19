public static class ComponentPaths
{
    public const string Master = "Custom";

    #region Player
    public const string Player = Master + "/Player";

    public const string PlayerController = Player + "/Player Controller";
    #endregion

    #region Movement
    public const string Movement = Master + "/Movement";

    public const string Mover = Movement + "/Mover";
    public const string Jumper = Movement + "/Jumper";
    public const string Croucher = Movement + "/Croucher";
    public const string Grappler = Movement + "/Grappler";
    #endregion

    #region Menu
    public const string Menu = Master + "/Menu";

    public const string SceneMenuItem = Menu + "/Scene Menu Item";
    public const string ExitMenuItem = Menu + "/Exit Menu Item";
    #endregion

    public const string Camera = Master + "/Camera";
}
public static class AssetPaths
{
    #region Data
    public const string Data = "Data";
    
    public const string MoverData = Data + "/Mover";
    public const string JumperData = Data + "/Jumper";
    public const string CroucherData = Data + "/Croucher";
    #endregion
}