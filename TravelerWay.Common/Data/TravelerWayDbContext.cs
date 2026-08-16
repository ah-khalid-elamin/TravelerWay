using Microsoft.EntityFrameworkCore;
using TravelerWay.Common.Entities;

namespace TravelerWay.Api.Data;

public class TravelerWayDbContext : DbContext
{
    public TravelerWayDbContext(DbContextOptions<TravelerWayDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderCancellation> OrderCancellations { get; set; } = null!;
    public DbSet<OrderChangeRequest> OrderChangeRequests { get; set; } = null!;
    public DbSet<OrderChangeOffer> OrderChangeOffers { get; set; } = null!;
    public DbSet<OrderChange> OrderChanges { get; set; } = null!;
    public DbSet<AirlineCredit> AirlineCredits { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints

    }
}
