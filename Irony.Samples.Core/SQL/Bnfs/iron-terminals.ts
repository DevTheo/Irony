
console.log("starting")

const reNonTerminals = /[<]([^>]*)[>]/g;
const reNonTerminalsDefinitions = /^[<]([^>]*)[>] ::=/gm;
const reStartsWithDigit = /^\d/;
const reAllSpaces = /[^\w]/g;
const digitPrefix = "n_";

const run = async () => {
    console.log("running")
    // if(Deno.args.length === 0)
    // {
    //     return;
    // }

    const fileName = Deno.args.length === 0 && (Deno.args[0] || "").length > 0 ? 
        Deno.args[0] : 
        "./sql-92.bnf";
    console.log(`fileName: ${fileName}`);
    const file_data = new TextDecoder("utf-8").decode(await Deno.readFileSync(fileName));
    
    console.log(`file_data length: ${file_data.length}`);
    const outputTerminals = [] as string[];
    const outputRules = [] as string[];
    const lines = file_data.split("\n");
    let i=0;
    while(i<lines.length)
    {
        const match = reNonTerminalsDefinitions.exec(lines[i]);
        if(match)
        {
            const nonTerminalCs = (reStartsWithDigit.exec(match[1]) ? digitPrefix : "") + 
                                    match[1].replace(reAllSpaces, "_");
            outputTerminals.push(`var ${nonTerminalCs} = new NonTerminal("${nonTerminalCs}");`);
        }
        

        i++;
    }
    await Deno.writeTextFile(`./${fileName}.txt`, outputTerminals.join("\n"));
};

run().then(() => console.log("done"));
