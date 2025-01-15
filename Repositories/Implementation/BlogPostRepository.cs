using System;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext dbContext;
    public BlogPostRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();
        return blogPost;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await dbContext.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id)
    {
        var existentBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        if(existentBlogPost is null)
            return null;
        return existentBlogPost;
    }
}
