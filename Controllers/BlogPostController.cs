using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    
    //[Route("api/[controller]/[Action]/")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        IBlogPostRepository blogPostRepository;
        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO request)
        {
            var blogPost = new BlogPost
            {
                Title= request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                UrlHandle = request.UrlHandle,
                Author = request.Author
            };

            blogPost = await blogPostRepository.CreateAsync(blogPost);
            var response = new BlogPostDto {
                Id = blogPost.Id,
                Title= blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var result = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                result.Add(new BlogPostDto{
                    Id = blogPost.Id,
                    Title= blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    UrlHandle = blogPost.UrlHandle,
                    Author = blogPost.Author
                });
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostsById(Guid id)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(id);
            if(blogPost is null)
                return NotFound();
                
            var result = new BlogPostDto{
                    Id = blogPost.Id,
                    Title= blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    UrlHandle = blogPost.UrlHandle,
                    Author = blogPost.Author
                };
            return Ok(result);
        }
    }
}
