using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repostiory
{
	public interface IRepository<ModelT, KeyT> where ModelT : class
	{
		Task<IEnumerable<ModelT>> GetAll();
		Task Insert(ModelT model);
		Task Update(ModelT old, ModelT updated = null);
		Task Delete(ModelT model);
		Task<ModelT> Get(KeyT id);
		Task SaveChanges();
	}
}
