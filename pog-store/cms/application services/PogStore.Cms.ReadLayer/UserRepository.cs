//using PogStore.Cms.Domain.Model.Users;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;

//namespace PogStore.Cms.ReadLayer {

//	public class UserRepository : IUserRepository {

//		private readonly DbContext _context;
//		private readonly IDbSet<User> _userDbSet;

//		public UserRepository(DbContext context) {
//			this._context = context;
//			this._userDbSet = _context.Set<User>();
//		}

//		public void Save(User user) {
//			_userDbSet.AddOrUpdate(user);
//		}
//	}
//}
