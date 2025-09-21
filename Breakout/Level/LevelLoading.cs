namespace Breakout.Level;

using System.Reflection;
using System.IO;
using System.Numerics;
using DIKUArcade.Entities;
using Breakout.Blocks;

public class LevelLoader {
    public static EntityContainer<Block> LoadLevel(string path) {

        string content = "";
        var stream =
            Assembly.GetCallingAssembly().GetManifestResourceStream(path);
        using (StreamReader reader = new StreamReader(stream!)) {
            content = reader.ReadToEnd();
        }

        string mapContent = LoadContent(content, "Map");
        string legendContent = LoadContent(content, "Legend");
        string metaContent = LoadContent(content, "Meta");


        List<(char, string, BlockType)> blockTypes = GetBlockMetadata(metaContent, legendContent);

        int i = 0;
        int j = 0;

        List<string> mapList = new List<string>(mapContent.Split('\n'));

        var blocks = new EntityContainer<Block>();
        Factory factory = new Factory();

        int items = mapList[1].Length - 1;
        float px = 0.0f; //padding x-axis
        float py = 0.1f; //padding x-axis
        float width = 1f / items - px / items;
        float height = width / 2.8f;

        while (j < mapList.Count) {

            if (i >= mapList[j].Length) {
                i = 0;
                j++;
            } else {
                foreach ((char, string, BlockType) a in blockTypes) {
                    if (mapList[j][i] == a.Item1) {
                        var newShape = new DynamicShape(new Vector2(px / 2 + i * width, 1 - j * height - py), new Vector2(width, height));
                        Block b = factory.CreateBlock((a.Item2, a.Item3), newShape);
                        b.BlockContainer = blocks;
                        b.PosX = i;
                        b.PosY = j;
                        b.SpritePath = a.Item2;
                        blocks.AddEntity(b);
                    }
                }
                i++;
            }

        }
        return blocks;
    }

    private static string LoadContent(string context, string key) {
        string startToken = key + ":";
        string endToken = "\n" + key + "/";

        int startIndex = context.IndexOf(startToken);
        if (startIndex == -1)
            throw new Exception($"Start token '{startToken.Trim()}' not found");

        startIndex += startToken.Length;

        int endIndex = context.IndexOf(endToken, startIndex);
        if (endIndex == -1)
            throw new Exception($"End token '{endToken.Trim()}' not found");

        return context.Substring(startIndex, endIndex - startIndex);
    }

    private static List<(char, string, BlockType)> GetBlockMetadata(string meta, string legend) {
        var blockMetadata = new List<(char symbol, string imagePath, BlockType type)>();
        var specialTypes = new Dictionary<char, BlockType> { };

        foreach (var blockType in Enum.GetValues<BlockType>()) {
            specialTypes[GetMetaValue(meta, blockType.ToString())] = blockType;
        }

        var legendLines = legend.Split('\n')
            .Where(line => line.Contains(')'));

        foreach (var line in legendLines) {
            var parts = line.Split(')', 2);
            if (parts.Length != 2)
                continue;

            var symbol = parts[0].Trim()[0];
            var imagePath = parts[1].Trim();

            var type = specialTypes.TryGetValue(symbol, out var foundType)
                ? foundType
                : BlockType.Normal;
            blockMetadata.Add((symbol, imagePath, type));
        }
        return blockMetadata;
    }
    private static char GetMetaValue(string text, string key) {
        var line = text.Split('\n')
            .FirstOrDefault(l => l.StartsWith(key + ":"));
        return line?.Split(':')[1].Trim()[0] ?? '?';
    }

    public static int GetTimerTime(string path) {
        string content = "";
        var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(path);
        using (StreamReader reader = new StreamReader(stream!)) {
            content = reader.ReadToEnd();
        }

        string metaContent = LoadContent(content, "Meta");

        // Find the line that starts with "Time:"
        foreach (string line in metaContent.Split('\n')) {
            if (line.Trim().StartsWith("Time:")) {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int time)) {
                    return time;
                }
            }
        }

        // If no valid time was found, return a default (like 0)
        return 0;
    }
}