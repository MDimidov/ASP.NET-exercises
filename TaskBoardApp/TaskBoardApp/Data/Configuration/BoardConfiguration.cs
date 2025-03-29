﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Data.Configuration
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasData(Seeds.OpenBoard, Seeds.InProgressBoard, Seeds.DoneBoard);
        }
    }
}
