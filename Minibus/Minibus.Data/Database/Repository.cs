using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace Minibus.Data.Database
{
	public class Repository<TModel> : IRepository<TModel>, IDisposable
		where TModel : class
	{
		private readonly MinibusContext _context;
		private DbSet<TModel> _dataStorage;

		public Repository()
		{
			_context = new MinibusContext();
			_dataStorage = MinibusContext.GetDataStorage<TModel>(_context);
		}

		#region IRepository

		public IQueryable<TModel> Get()
		{
			return _dataStorage;
		}

		public void Add(TModel ent)
		{
			_dataStorage.Add(ent);
		}

		public void Update(TModel ent, object[] properties)
		{
			_dataStorage.AddOrUpdate(ent);
			foreach (var property in properties)
			{
				var propertyModel = _context.Entry(property);
				propertyModel.State = EntityState.Modified;
			}
		}

		public void Attach(TModel ent)
		{
			_dataStorage.Attach(ent);
			_context.Entry(ent).State = EntityState.Modified;
		}

		public void AddOrUpdateRange(IEnumerable<TModel> ents)
		{
			foreach (var ent in ents)
			{
				_dataStorage.AddOrUpdate(ent);
			}
		}

		public void Remove(TModel ent, object[] properties)
		{
			var dbEntityEntry = _context.Entry(ent);
			dbEntityEntry.State = EntityState.Deleted;

			foreach (var property in properties)
			{
				var propertyModel = _context.Entry(property);
				propertyModel.State = EntityState.Deleted;
			}
		}

		public void RemoveRange(IEnumerable<TModel> ents)
		{
			foreach (var ent in ents)
			{
				var dbEntityEntry = _context.Entry(ent);
				dbEntityEntry.State = EntityState.Deleted;
			}
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}

		#endregion IRepository


		public void Dispose()
		{
			_dataStorage = null;
			_context.Dispose();
		}
	}
}
