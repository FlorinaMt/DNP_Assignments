﻿using System.Security.Principal;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private IUserRepository userRepository;
    private ICommentRepository commentRepository;
    private IPostRepository postRepository;
    private ILikeRepository likeRepository;

    public ManageUsersView(IUserRepository userRepository,
        IPostRepository postRepository, ICommentRepository commentRepository,
        ILikeRepository likeRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.likeRepository = likeRepository;
    }
    
    public async Task OpenAsync()
    {
        string? userInput = Choose();

        while (userInput is null ||
               (!userInput.Equals("1") && !userInput.Equals("2")))
        {
            Console.WriteLine("Invalid input. Please try again.");
            userInput = Choose();
        }

        if (userInput.Equals("1"))
        {
            CreateUserView createUserView = new CreateUserView(userRepository, postRepository, commentRepository, likeRepository);
            await createUserView.OpenAsync();
        }
        else if (userInput.Equals("2"))
        {
            UserListView userListView = new UserListView(userRepository, this);
            await userListView.OpenAsync();
        }
    }

    private string? Choose()
    {
        Console.WriteLine("Choose 1 or 2: \n1. Create user. \n2. See the list of users.");
        return Console.ReadLine();
    }
}