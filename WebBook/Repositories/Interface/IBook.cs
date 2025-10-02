﻿using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public interface IBook
    {
        List<BookWithAuthorAndPublisherDTO> GetAllBooks(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 100);

        BookWithAuthorAndPublisherDTO? GetBookById(int id);
        AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
        AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
        Book? DeleteBookById(int id);
    }
}
