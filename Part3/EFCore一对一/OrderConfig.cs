using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对一
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("T_Orders");
            builder.HasOne(t => t.Delivery)
                .WithOne(t => t.Order)
                .HasForeignKey<Delivery>(t => t.OrderId)
                .IsRequired();
        }
    }
}
