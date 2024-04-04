﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Infrastructure.entity
{
    internal class BookEntityConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Book");
            builder.Property(e=> e.Title).HasMaxLength(50).IsRequired();
            builder.Property(e => e.AuthorName).HasMaxLength(20).IsRequired();

        }
    }
}
