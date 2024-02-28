using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to a collection of Entities. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
  /// </summary>
  public class EntityContainer : ContainerEnumerableBase<Entity>
  {
    internal EntityContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    /// <summary>
    /// The ObjectId of the container.
    /// </summary>
    public ObjectId ObjectId
      => ID;

    /// <summary>
    /// Adds an Entity to the container.
    /// </summary>
    /// <param name="entity">The Entity to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <returns>The ObjectId of the Entity that was added.</returns>
    public ObjectId Add(Entity entity, bool setDatabaseDefaults = false)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(entity, nameof(entity));
      Require.NewlyCreated(entity, nameof(entity));

      return AddInternal(entity, setDatabaseDefaults);
    }

    /// <summary>
    /// Adds Entities to the container.
    /// </summary>
    /// <param name="entities">The Entities to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <returns>The ObjectIds of the Entities that were added.</returns>
    public IReadOnlyCollection<ObjectId> AddRange(IEnumerable<Entity> entities, bool setDatabaseDefaults = false)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(entities, nameof(entities));

      var ids = new List<ObjectId>();
      foreach (var entity in entities)
      {
        Require.ElementNotNull(entity, nameof(entities));
        Require.NewlyCreated(entity, nameof(entity));
        ids.Add(AddInternal(entity, setDatabaseDefaults));
      }

      return ids;
    }

    /// <summary>
    /// Adds Entities to the container.
    /// </summary>
    /// <param name="item">The Entity to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <returns>The ObjectId of the Entity that was added.</returns>
    private ObjectId AddInternal(Entity item, bool setDatabaseDefaults)
    {
      var btr = (BlockTableRecord)transaction.GetObject(ID, OpenMode.ForWrite);

      if (setDatabaseDefaults)
      {
        item.SetDatabaseDefaults();
      }

      var id = btr.AppendEntity(item);
      transaction.AddNewlyCreatedDBObject(item, true);

      return id;
    }

    /// <summary>
    /// Removes all Entities from this container.
    /// </summary>
    public void Clear()
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      foreach (var entity in this.UpgradeOpen())
      {
        entity.Erase();
      }
    }
  }
}
