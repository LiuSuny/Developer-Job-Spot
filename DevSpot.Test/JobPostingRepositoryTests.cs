using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevSpot.Test
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb").Options;
        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db

            //act (
            // //job posting)


            var repository = new JobPostingRepository(db); //creating an instance of repository

            var jobPosting = new JobPosting
            {
                title = "Test title",
                Description = "Test description",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test Location",
                UserId = "TestUserId"
            };

            //execute
            await repository.AddAsync(jobPosting);
            
            //check
            //(results
            var result = db.JobPostings.Find(jobPosting.Id);
            //assert)

            Assert.NotNull(result);
            Assert.Equal("Test company", result.Company);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db

            //act (
            // //job posting)


            var repository = new JobPostingRepository(db); //creating an instance of repository

            var jobPosting = new JobPosting
            {
                title = "Test title adding",
                Description = "Test description",
                PostedDate = DateTime.Now,
                Company = "Test company",
                Location = "Test Location",
                UserId = "TestUserId"
            };

            //execute           
            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await repository.GetByIdAsync(jobPosting.Id);
            Assert.NotNull(result);
            Assert.Equal("Test title adding", result.title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowExceptionJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db
            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            repository.GetByIdAsync(999));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db

            //act (
            // //job posting)


            var repository = new JobPostingRepository(db); //creating an instance of repository

            var jobPosting1 = new JobPosting
            {
                title = "Test title1",
                Description = "Test description1",
                PostedDate = DateTime.Now,
                Company = "Test company1",
                Location = "Test Location1",
                UserId = "TestUserId1"
            };
            var jobPosting2 = new JobPosting
            {
                title = "Test title2",
                Description = "Test description2",
                PostedDate = DateTime.Now,
                Company = "Test company2",
                Location = "Test Location2",
                UserId = "TestUserId2"
            };

            //execute           
            await db.JobPostings.AddRangeAsync(jobPosting1, jobPosting2);          
            await db.SaveChangesAsync();

            var result = await repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.True( result.Count() >=2);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAllJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db

            //act (
            // //job posting)


            var repository = new JobPostingRepository(db); //creating an instance of repository

            var jobPosting1 = new JobPosting
            {
                title = "Test title1",
                Description = "Test description1",
                PostedDate = DateTime.Now,
                Company = "Test company1",
                Location = "Test Location1",
                UserId = "TestUserId1"
            };
           
            //execute           
            await db.JobPostings.AddAsync(jobPosting1);
            await db.SaveChangesAsync();

            jobPosting1.Description = "Test description1 Update";
             await repository.UpdateAsync(jobPosting1);
            var result = await db.JobPostings.FindAsync(jobPosting1.Id);
            Assert.NotNull(result);
            Assert.Equal("Test description1 Update", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPosting()
        {
            //Assign(
            //db context

            var db = CreateDbContext(); //creating our db

            //act (
            // //job posting)


            var repository = new JobPostingRepository(db); //creating an instance of repository

            var jobPosting1 = new JobPosting
            {
                title = "Test title1",
                Description = "Test description1",
                PostedDate = DateTime.Now,
                Company = "Test company1",
                Location = "Test Location1",
                UserId = "TestUserId1"
            };

            //execute           
            await db.JobPostings.AddAsync(jobPosting1);
            await db.SaveChangesAsync();

            await repository.DeleteAsync(jobPosting1.Id);
            var result = await db.JobPostings.FindAsync(jobPosting1.Id);

            Assert.Null(result);
           
        }
    }
}