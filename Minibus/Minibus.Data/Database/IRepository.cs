using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minibus.Data.Database
{
	public interface IRepository<TModel>
		where TModel : class
	{
		IQueryable<TModel> Get();
		void Add(TModel add);
		void Attach(TModel ent);
		void Update(TModel add, params object[] properties);
		void Remove(TModel ent, params object[] properties);
		void RemoveRange(IEnumerable<TModel> ents);
		void AddOrUpdateRange(IEnumerable<TModel> ents);
		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
