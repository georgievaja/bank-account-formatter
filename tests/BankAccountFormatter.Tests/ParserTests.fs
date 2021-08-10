namespace BankAccountFormatter.Parser.Tests

open Xunit.Abstractions
open BankAccountFormatter.Parser.ParserTypes.Monads
open BankAccountFormatter.Parser.AbstractSyntaxTree
open FluentAssertions

module ParserTests =

    open Xunit
    open BankAccountFormatter.Parser.Parser

    type ParserTests(output:ITestOutputHelper) =

        [<Fact>]
        member __.``Empty string should return empty collection``() =
            let result = parse ""
            let expectedAst =
                BankAccountFormatParts([])

            result.Should().Be(ParsingResult.Success expectedAst, null)

        [<Theory>]
        [<InlineData('k')>]
        [<InlineData(' ')>]
        [<InlineData(':')>]
        member __.``Other chars should be included`` otherChar =
            let result = parse ("pa"+otherChar)
            let expectedAst =
                BankAccountFormatParts(
                     [BankAccountPart(Prefix(MinimizedPrefix));
                      BankAccountPart(AccountNumber(MinimizedAccountNumber));
                      OtherChar ((char)otherChar)])

            result.Should().Be(ParsingResult.Success expectedAst, null)
                       
                
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

        [<Theory>]
        [<InlineData("PAB")>]
        member __.``AST represented: PAB`` data =
            let result = parse data
        
            let expectedAst =
                 BankAccountFormatParts(
                     [BankAccountPart(Prefix(PaddedPrefix));
                      BankAccountPart(AccountNumber(PaddedAccountNumber))
                      BankAccountPart(BankCode(PaddedBankCode))])

            match result with
            | (ParsingResult.Success s) -> Assert.Equal(expectedAst, s)
            | (ParsingResult.Failure f) ->              
                output.WriteLine(sprintf "%A" f)
                Assert.True(false)