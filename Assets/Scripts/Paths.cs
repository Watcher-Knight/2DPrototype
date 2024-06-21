public static class ComponentPaths
{
    public const string Master = "Custom";

    #region Player
    public const string Player = Master + "/Player";

    public const string PlayerController = Player + "/Player Controller";
    public const string PlayerEventHandler = Player + "/Player Event Handler";
    public const string PlayerAnimator = Player + "/Player Animator";
    public const string PlayerAnimatorController = Player + "/Player Animator Controller";
    #endregion

    #region Movement
    public const string Movement = Master + "/Movement";

    public const string Mover = Movement + "/Mover";
    public const string Jumper = Movement + "/Jumper";
    public const string Croucher = Movement + "/Croucher";
    public const string Grappler = Movement + "/Grappler";
    public const string Magnet = Movement + "/Magnet";
    #endregion

    #region Combat
    public const string Combat = Master + "/Combat";

    public const string Shooter = Combat + "/Shooter";
    public const string Projectile = Combat + "/Projectile";
    #endregion

    #region Menu
    public const string Menu = Master + "/Menu";

    public const string SceneMenuItem = Menu + "/Scene Menu Item";
    public const string ExitMenuItem = Menu + "/Exit Menu Item";
    #endregion

    public const string Camera = Master + "/Camera";
    public const string GrapplePoint = Master + "/GrapplePoint";
    public const string Magnetable = Master + "/Magnetable";
    public const string Aimer = Master + "/Aimer";
}

public static class AssetPaths
{
    #region Data
    public const string Data = "Data";
    
    public const string MoverData = Data + "/Mover";
    public const string JumperData = Data + "/Jumper";
    public const string CroucherData = Data + "/Croucher";
    public const string ShooterData = Data + "/Shooter";
    public const string ProjectileData = Data + "/Projectile";
    public const string GrapplerData = Data + "/Grappler";
    public const string MagnetData = Data + "/Magnet";
    public const string PlayerEventHandlerData = Data + "/Player Event Handler";
    public const string CheckPointData = Data + "/CheckPoint";
    #endregion
}