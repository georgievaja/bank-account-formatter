namespace BankAccountParser.Parser.Tests

open Xunit.Abstractions
open Xunit.Sdk
open BankAccountParser.Parser.ParserTypes.Monads
open BankAccountParser.Parser.ParserTypes.Types
open BankAccountParser.Parser.AbstractSyntaxTree

module ParserTests =

    open System
    open Xunit
    open FParsec
    open BankAccountParser.Parser.Parser

    type ParserTests(output:ITestOutputHelper) =

        [<Theory>]
        [<InlineData("p-ca/b")>]
        member __.``Parsing should fail`` data =
            let result = parse data
            match result with
            | (ParsingResult.Success _) -> Assert.True(false) 
            | (ParsingResult.Failure _) -> Assert.True(true)                         
                
        [<Theory>]
        [<InlineData("p-a/b")>]
        member __.``AST represented: p-a/b`` data =
            let result = parse data
        
            let expectedAst =
                 BankAccountFormatParts(
                     [BankAccountPart(Prefix(MinimizedPrefix));
                      BankAccountSeparator(PrefixSeparator);
                      BankAccountPart(AccountNumber(MinimizedAccountNumber))
                      BankAccountSeparator(BankCodeSeparator)
                      BankAccountPart(BankCode(MinimizedBankCode))])

            match result with
            | (ParsingResult.Success s) -> Assert.Equal(expectedAst, s)
            | (ParsingResult.Failure f) ->              
                output.WriteLine(sprintf "%A" f)
                Assert.True(false)