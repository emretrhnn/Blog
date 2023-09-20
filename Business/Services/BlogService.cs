using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IBlogService : IService<BlogModel>
    {
    }

    public class BlogService : IBlogService
    {
        private readonly RepoBase<Blog> _blogRepo;

        public BlogService(RepoBase<Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public IQueryable<BlogModel> Query()
        {
           return _blogRepo.Query().OrderByDescending(b => b.Score).ThenByDescending(b => b.DinosaurName)
                .Select(b => new BlogModel()
                {
                    DinosaurName = b.DinosaurName,
                    Score = b.Score,
                    Features = b.Features,
                    Guid = b.Guid,
                    Id = b.Id,
                    UserId = b.UserId,

                    UserNameDisplay = b.User.UserName,
                    TagsDisplay = b.BlogTags.Select(bt => new TagModel()
                    {
                        Guid = bt.Tag.Guid,
                        Name = bt.Tag.Name,
                        Id = bt.Tag.Id
                    }).ToList(),

                    TagIds = b.BlogTags.Select(bt => bt.TagId).ToList()
                });
        }
        public Result Add(BlogModel model)
        {
            if (_blogRepo.Query().Any(b => b.UserId == model.UserId &&
                b.DinosaurName.ToLower() == model.DinosaurName.ToLower().Trim()))
            {
                return new ErrorResult("Blog with the same dinasour name exists!");
            }

            Blog entity = new Blog()
            {
                DinosaurName = model.DinosaurName,
                Score = model.Score,
                Features = model.Features,

                UserId = model.UserId.Value,

                BlogTags = model.TagIds.Select(tagId => new BlogTag()
                {
                    TagId = tagId
                }).ToList()
            };

            _blogRepo.Add(entity);

            return new SuccessResult("Blog added successfully.");


        }

        public Result Delete(int id)
        {
            _blogRepo.Delete<BlogTag>(bt => bt.BlogId == id);

            _blogRepo.Delete(id);

            return new SuccessResult("Blog deleted successfully.");
        }

        public void Dispose()
        {
            _blogRepo.Dispose();
        }


        public Result Update(BlogModel model)
        {
            if(_blogRepo.Query().Any(b => b.UserId == model.UserId 
            && b.DinosaurName.ToLower().Trim() == model.DinosaurName.ToLower().Trim()
            && b.Id != model.Id))
            {
				return new ErrorResult("Blog with the same name exists!");
			}

            var BlogEntity = _blogRepo.Query(true).Include(b => b.BlogTags).SingleOrDefault(b => b.Id == model.Id);

            var existingBlogIds = BlogEntity.BlogTags.Select(blogTag => blogTag.BlogId);

            _blogRepo.Delete<BlogTag>(blogTag => existingBlogIds.Contains(blogTag.BlogId));
            
            var Entity = new Blog()
            {
                Id = model.Id,
                DinosaurName = model.DinosaurName,
                Features = model.Features,
                Score = model.Score,
                UserId = model.UserId.Value,
                BlogTags = model.TagIds.Select(tagId => new BlogTag()
                { 
                    TagId = tagId 
                }).ToList()
            };

            _blogRepo.Update(Entity);

            return new SuccessResult("Dinasour updated successfully");
        }
    }
}
