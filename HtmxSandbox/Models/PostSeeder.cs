namespace HtmxSandbox.Models;

internal class PostSeeder
{
    // @formatter:wrap_lines False
    private static readonly DbPost[] Samples =
    [
        new() { Title = "Thanks for all the fish!", Content = "The answer to life, the universe, and everything is 42.", Author = "Adams" },
        new() { Title = "Hello World", Content = "Every journey in programming starts with a simple greeting.", Author = "Kernighan" },
        new() { Title = "Coffee First", Content = "No bugs should be investigated before the first cup of coffee.", Author = "Developer" },
        new() { Title = "Clean Code", Content = "Readable code is easier to maintain and extend.", Author = "Martin" },
        new() { Title = "Unit Tests Matter", Content = "Tests provide confidence when changing code.", Author = "Kent" },
        new() { Title = "Async All the Things", Content = "Non-blocking code improves scalability.", Author = "Tom" },
        new() { Title = "Naming Things", Content = "There are only two hard things in computer science.", Author = "Phil" },
        new() { Title = "Rubber Duck", Content = "Explaining the problem often reveals the solution.", Author = "Dave" },
        new() { Title = "Refactor Friday", Content = "Small improvements add up over time.", Author = "Alice" },
        new() { Title = "Keep It Simple", Content = "Complexity should be introduced only when necessary.", Author = "Tom" },

        new() { Title = "Documentation Day", Content = "Future developers will appreciate clear documentation.", Author = "Carol" },
        new() { Title = "Deploy With Care", Content = "Production deserves respect and backups.", Author = "Eve" },
        new() { Title = "Version Control", Content = "Commit early and commit often.", Author = "Linus" },
        new() { Title = "Database Indexes", Content = "Performance often starts with proper indexing.", Author = "Grace" },
        new() { Title = "Caching Wisdom", Content = "Avoid doing expensive work twice.", Author = "Frank" },
        new() { Title = "Logging Matters", Content = "Good logs make debugging easier.", Author = "Henry" },
        new() { Title = "Monitor Everything", Content = "You cannot improve what you cannot measure.", Author = "Grace" },
        new() { Title = "API Design", Content = "Consistency is more valuable than cleverness.", Author = "Jack" },
        new() { Title = "RESTful Thoughts", Content = "Resources should be predictable and discoverable.", Author = "Kim" },
        new() { Title = "Concurrency", Content = "Parallelism introduces both speed and complexity.", Author = "Lee" },

        new() { Title = "Memory Leaks", Content = "Unused objects should not outlive their purpose.", Author = "Mia" },
        new() { Title = "Design Patterns", Content = "Patterns are tools, not rules.", Author = "Fiona" },
        new() { Title = "Open Source", Content = "Sharing knowledge benefits everyone.", Author = "Oscar" },
        new() { Title = "Binary Trees", Content = "Data structures shape algorithm performance.", Author = "Paul" },
        new() { Title = "Sorting Things Out", Content = "Efficiency matters when data grows.", Author = "Quinn" },
        new() { Title = "Network Latency", Content = "The speed of light is a stubborn limitation.", Author = "Rita" },
        new() { Title = "Cloud Native", Content = "Infrastructure should be resilient and automated.", Author = "Sam" },
        new() { Title = "Containers Everywhere", Content = "Isolation simplifies deployment.", Author = "Tom" },
        new() { Title = "Microservices", Content = "Distributed systems trade simplicity for flexibility.", Author = "Uma" },
        new() { Title = "Monoliths", Content = "Sometimes one application is enough.", Author = "Victor" },

        new() { Title = "Weekend Project", Content = "Side projects are great learning opportunities.", Author = "Wendy" },
        new() { Title = "Keyboard Shortcuts", Content = "Small efficiencies save time every day.", Author = "Xavier" },
        new() { Title = "Dark Mode", Content = "Preferences are deeply personal.", Author = "Grace" },
        new() { Title = "The Merge Conflict", Content = "Communication is just as important as code.", Author = "Zack" },
        new() { Title = "Debugging Session", Content = "Half the battle is reproducing the issue.", Author = "Anna" },
        new() { Title = "Feature Flags", Content = "Controlled rollouts reduce risk.", Author = "Ben" },
        new() { Title = "Code Reviews", Content = "Feedback improves software quality.", Author = "Chris" },
        new() { Title = "Technical Debt", Content = "Shortcuts eventually require repayment.", Author = "Diana" },
        new() { Title = "Immutable Data", Content = "Predictability simplifies reasoning.", Author = "Eric" },
        new() { Title = "Functional Style", Content = "Pure functions are easier to test.", Author = "Fiona" },

        new() { Title = "Game Night", Content = "Not every challenge needs a compiler.", Author = "George" },
        new() { Title = "Morning Walk", Content = "Fresh air often inspires fresh ideas.", Author = "Helen" },
        new() { Title = "Book Recommendation", Content = "Learning never really stops.", Author = "Ian" },
        new() { Title = "Music While Coding", Content = "Some bugs disappear with the right playlist.", Author = "Julia" },
        new() { Title = "Late Night Fix", Content = "The final bug appears after midnight.", Author = "Kevin" },
        new() { Title = "Backup Strategy", Content = "Hope is not a disaster recovery plan.", Author = "Laura" },
        new() { Title = "Continuous Integration", Content = "Automation catches mistakes early.", Author = "Mike" },
        new() { Title = "Release Day", Content = "Shipping software is always exciting.", Author = "Nora" },
        new() { Title = "Performance Tuning", Content = "Measure before optimizing.", Author = "Oliver" },
        new() { Title = "Reflections", Content = "Every line of code tells a story.", Author = "Grace" },

        new() { Title = "The Journey", Content = "From idea to implementation, the path is rarely straight.", Author = "Zack" },
        new() { Title = "Legacy Code", Content = "Maintaining old code is a skill in itself.", Author = "George" },
        new() { Title = "Collaboration", Content = "Great software is built by teams, not individuals.", Author = "Anna" },
        new() { Title = "Innovation", Content = "The best solutions often come from unexpected places.", Author = "Helen" },
        new() { Title = "Persistence", Content = "Debugging is a test of patience and perseverance.", Author = "Eric" },
        new() { Title = "Learning", Content = "Every bug is an opportunity to learn something new.", Author = "Fiona" },
        new() { Title = "Community", Content = "Sharing knowledge strengthens the entire ecosystem.", Author = "Oscar" },
        new() { Title = "Creativity", Content = "Sometimes the best code is the simplest code.", Author = "Julia" },
        new() { Title = "Balance", Content = "A well-rested mind writes better code.", Author = "Helen" },
        new() { Title = "Future", Content = "The only constant in software development is change.", Author = "Carol" },

        new() { Title = "Elegance", Content = "The best code is the code that solves the problem implicitly.", Author = "Fiona" },
        new() { Title = "Simplicity", Content = "Complexity is the enemy of execution", Author = "Grace" },
        new() { Title = "Legacy Systems", Content = "Understanding the past is key to improving the future.", Author = "Diana" },
        new() { Title = "Debugging Marathon", Content = "Sometimes the solution is found in the most unexpected places.", Author = "Kevin" },
        new() { Title = "Code Golf", Content = "Writing the shortest code is a fun challenge, but not always practical.", Author = "Laura" },
        new() { Title = "Refactoring", Content = "Improving code structure without changing its behavior is an art.", Author = "Mike" },
        new() { Title = "Final Touches", Content = "Polishing the code before release is crucial.", Author = "Nina" },
        new() { Title = "The Last Commit?", Content = "No project has its final line of code.", Author = "Linus" },
        new() { Title = "The End", Content = "And so, the code lives on, evolving with each new line.", Author = "Grace" },
        new() { Title = "Final Thoughts", Content = "Software evolves one commit at a time.", Author = "Quinn" }
    ];
    // @formatter:wrap_lines restore

    public static IEnumerable<DbPost> Seed(uint count = 1)
    {
        var now = DateTime.Now;
        var posts = new Stack<DbPost>(Samples.Reverse());
        do
        {
            if (posts.Count == 0)
            {
                Random.Shared.Shuffle(Samples);
                posts = new Stack<DbPost>(Samples);
            }

            var post = posts.Pop();
            post.Created = now.AddSeconds(-count);
            yield return post;
        } while (--count > 0);
    }
}