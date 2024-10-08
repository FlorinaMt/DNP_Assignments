﻿using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class LikeInMemoryRepository:ILikeRepository
{
    private List<Like> likes;

    public LikeInMemoryRepository()
    {
        likes = new List<Like>();
        AddLikeAsync(new Like{PostId = 1, UserId = 2});
        AddLikeAsync(new Like{PostId = 1, UserId = 3});
        AddLikeAsync(new Like{PostId = 1, UserId = 4});

        
        AddLikeAsync(new Like{PostId = 2, UserId = 2});
        AddLikeAsync(new Like{PostId = 2, UserId = 5});
        
        AddLikeAsync(new Like{PostId = 3, UserId = 4});
        
        AddLikeAsync(new Like{PostId = 4, UserId = 1});
        AddLikeAsync(new Like{PostId = 4, UserId = 2}); 
        AddLikeAsync(new Like{PostId = 4, UserId = 3});
        AddLikeAsync(new Like{PostId = 4, UserId = 5});

        AddLikeAsync(new Like{PostId = 5, UserId = 1});
        AddLikeAsync(new Like{PostId = 5, UserId = 2});
        AddLikeAsync(new Like{PostId = 5, UserId = 4});
    }
    
    public async Task<Like> AddLikeAsync(Like like)
    {
        for(int i=0; i<likes.Count; i++)
            if(likes[i].PostId == like.PostId && likes[i].UserId == like.UserId)
                throw new InvalidOperationException("You cannot add likes more than once.");
        like.LikeId = likes.Any() ? likes.Max(l => l.LikeId) + 1 : 1;
        likes.Add(like);
        return like;
    }

    public async Task DeleteLikeAsync(Like like)
    {
        Like likeToRemove = GetLikeByIdAsync(like.LikeId).Result;
        likes.Remove(likeToRemove);
    }

    public async Task<Like> GetLikeByIdAsync(int id)
    {
        Like? foundLike =
            likes.SingleOrDefault(l => l.LikeId == id);
        if(foundLike is null)
            throw new InvalidOperationException("No like found");
        return foundLike;
    }

    public IQueryable<Like> GetLikesForPost(int postId)
    {
        List<Like> foundLikes =
            likes.FindAll(l => l.PostId == postId);
        if(foundLikes.Count==0)
            throw new InvalidOperationException("No likes for this post.");
        return foundLikes.AsQueryable();
    }

    public IQueryable<Like> GetAllLikes()
    {
        return likes.AsQueryable();
    }

}