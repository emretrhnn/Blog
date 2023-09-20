using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface  ITagService : IService<TagModel>
    {
		List<TagModel> GetList();
		TagModel GetItem(int id);
	}

    public class TagService : ITagService
    {
        private readonly RepoBase<Tag> _tagRepo;

        public TagService(RepoBase<Tag> tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public Result Add(TagModel model)
        {
            if (_tagRepo.Query().Any(t => t.Name == model.Name.ToLower().Trim()))
                return new ErrorResult("Tag with the same name exists");

            Tag entity = new Tag()
            {
                Name = model.Name.Trim(),
            };

            _tagRepo.Add(entity);

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            _tagRepo.Delete<BlogTag>(bt => bt.TagId == id);

            _tagRepo.Delete(t => t.Id == id);

            return new SuccessResult(); 
        }

        public void Dispose()
        {
            _tagRepo.Dispose();
        }

		public List<TagModel> GetList()
		{
			return Query().OrderBy(t => t.Name).Select(t => new TagModel()
			{
				Guid = t.Guid,
				Id = t.Id,
				Name = t.Name,
                
			}).ToList();
		}

		public TagModel GetItem(int id)
		{
			
			var model = Query().Select(t => new TagModel()
			{
				Guid = t.Guid,
				Id = t.Id,
				Name = t.Name,
                
			}).SingleOrDefault(t => t.Id == id);

			return model;
		}

		public IQueryable<TagModel> Query()
        {
            return _tagRepo.Query().OrderBy(t => t.Name).Select(t => new TagModel()
            {
                Guid = t.Guid,
                Name = t.Name,
                Id = t.Id,
            });
        }

        public Result Update(TagModel model)
        {
			if (_tagRepo.Query().Any(t => t.Name == model.Name.ToLower().Trim()))
				return new ErrorResult("Tag with the same name exists");

            Tag entity = _tagRepo.GetItem(model.Id);
            entity.Name = model.Name.Trim();
            _tagRepo.Update(entity);

            return new SuccessResult();
		}
    }
}
