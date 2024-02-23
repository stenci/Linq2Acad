using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace Linq2Acad
{
  public class NotDisposableAcadDatabase
  {
    private readonly AcadDatabase db;

    public NotDisposableAcadDatabase(Transaction transaction)
    {
      this.db = AcadDatabase.Active(transaction, false, false);
    }

    /// <summary>
    /// The drawing database in use.
    /// </summary>
    public Database Database { get => this.db.Database; }

    /// <summary>
    /// Provides access to the summary info.
    /// </summary>
    public AcadSummaryInfo SummaryInfo { get => this.db.SummaryInfo; }

    /// <summary>
    /// Provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.
    /// </summary>
    public DbObjectContainer DbObjects { get => this.db.DbObjects; }

    /// <summary>
    /// Provides access to all style related tables and dictionaries.
    /// </summary>
    public StylesContainer Styles { get => this.db.Styles; }

    /// <summary>
    /// Provides access to all XRef elements and methods to attach, overlay, resolve, reload and unload XRefs.
    /// </summary>
    public XRefContainer XRefs { get => this.db.XRefs; }

    #region Tables

    /// <summary>
    /// Provides access to the elements of the Block table and methods to create, add and import BlockTableRecords.
    /// </summary>
    public BlockContainer Blocks { get => this.db.Blocks; }

    /// <summary>
    /// Provides access to the elements of the Layer table and methods to create, add and import LayerTableRecords.
    /// </summary>
    public LayerContainer Layers { get => this.db.Layers; }

    /// <summary>
    /// Provides access to the elements of the Linetype table and methods to create, add and import LinetypeTableRecords.
    /// </summary>
    public LinetypeContainer Linetypes { get => this.db.Linetypes; }

    /// <summary>
    /// Provides access to the elements of the RegApp table and methods to create, add and import RegAppTableRecords.
    /// </summary>
    public RegAppContainer RegApps { get => this.db.RegApps; }

    /// <summary>
    /// Provides access to the elements of the Ucs table and methods to create, add and import UcsTableRecords.
    /// </summary>
    public UcsContainer Ucss { get => this.db.Ucss; }

    /// <summary>
    /// Provides access to the elements of the Viewport table and methods to create, add and import ViewportTableRecords.
    /// </summary>
    public ViewportContainer Viewports { get => this.db.Viewports; }

    /// <summary>
    /// Provides access to the elements of the View table and methods to create, add and import ViewTableRecords.
    /// </summary>
    public ViewContainer Views { get => this.db.Views; }

    #endregion

    #region Dictionaries

    /// <summary>
    /// Provides access to the elements of the Layout dictionary and methods to create, add and import Layouts.
    /// </summary>
    public LayoutContainer Layouts { get => this.db.Layouts; }

    /// <summary>
    /// Provides access to the elements of the Group dictionary and methods to create, add and import Groups.
    /// </summary>
    public GroupContainer Groups { get => this.db.Groups; }

    /// <summary>
    /// Provides access to the elements of the Material dictionary and methods to create, add and import Materials.
    /// </summary>
    public MaterialContainer Materials { get => this.db.Materials; }

    /// <summary>
    /// Provides access to the elements of the PlotSettings dictionary and methods to create, add and import PlotSettings.
    /// </summary>
    public PlotSettingsContainer PlotSettings { get => this.db.PlotSettings; }

    #endregion

    #region CurrentSpace | ModelSpace | PaperSpace

    /// <summary>
    /// Provides access to the entities of the currently active space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
    /// </summary>
    public EntityContainer CurrentSpace { get => this.db.CurrentSpace; }

    /// <summary>
    /// Provides access to the entities of the model space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
    /// </summary>
    public EntityContainer ModelSpace { get => this.db.ModelSpace; }

    /// <summary>
    /// Provides access to the entities of the paper space layouts.
    /// </summary>
    public IEnumerable<PaperSpaceEntityContainer> PaperSpace { get => this.db.PaperSpace; }

    #endregion

    #region Overrides to remove methods from IntelliSense

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Equals(object obj) => base.Equals(obj);

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int GetHashCode() => base.GetHashCode();

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override string ToString() => base.ToString();

    #endregion
  }
}
