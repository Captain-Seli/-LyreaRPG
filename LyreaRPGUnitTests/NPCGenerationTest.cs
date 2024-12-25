using LyreaRPG.AI;

[Test]
void GeneratePersonality_ShouldReturnValidDictionary()
{
    try
    {
        // Act
        Console.WriteLine("Starting GeneratePersonality Test...");
        var personality = NPCGenerator.GeneratePersonality();

        // Assert
        Assert.NotNull(personality, "Personality dictionary should not be null.");
        Assert.IsInstanceOf<Dictionary<string, string>>(personality, "Result should be a Dictionary<string, string>.");
        Assert.IsTrue(personality.ContainsKey("Trait"), "Personality should contain a 'Trait'.");
        Assert.IsTrue(personality.ContainsKey("Ideal"), "Personality should contain an 'Ideal'.");
        Assert.IsTrue(personality.ContainsKey("Bond"), "Personality should contain a 'Bond'.");
        Assert.IsTrue(personality.ContainsKey("Flaw"), "Personality should contain a 'Flaw'.");
        Assert.IsNotEmpty(personality["Trait"], "'Trait' should not be empty.");
        Assert.IsNotEmpty(personality["Ideal"], "'Ideal' should not be empty.");
        Assert.IsNotEmpty(personality["Bond"], "'Bond' should not be empty.");
        Assert.IsNotEmpty(personality["Flaw"], "'Flaw' should not be empty.");

        // Console Output for Debugging
        Console.WriteLine("Generated Personality:");
        foreach (var entry in personality)
        {
            Console.WriteLine($"  {entry.Key}: {entry.Value}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Test failed with exception: {ex.Message}");
        throw;
    }
}
