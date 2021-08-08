namespace BankAccountParser.Parser.Tests

open BankAccountParser.Parser.AbstractSyntaxTree
open BankAccountParser.Types.BankAccount.PublicTypes
open Xunit.Abstractions
open BankAccountParser.Parser.ParserTypes.Monads
open BankAccountParser.Interpreter.Interpreter

module InterpreterTests =
    open Xunit
    open BankAccountParser.Parser.Parser

    let testTeamData =
        {
            Prefix = 111
            AccountNumber = 10008888L
            BankCode = 200s
        }
        
    type InterpreterTests(output: ITestOutputHelper) =
        
        [<Theory>]
        [<InlineData("p-a/b", "111-10008888/200")>]
        [<InlineData("P-a/B", "000111-10008888/0200")>]
        member __.``Existence interpretation returns true`` data expectedResult =
            let result = parse data

            match result with
            | (ParsingResult.Success ast) -> 
                let func = interpret ast
                Assert.Equal(func testTeamData, expectedResult)
            | _ -> 
                failwith "parsing was unsucessfull"

