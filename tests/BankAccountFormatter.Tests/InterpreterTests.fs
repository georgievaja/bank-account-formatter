namespace BankAccountFormatter.Parser.Tests

open BankAccountFormatter.Parser.AbstractSyntaxTree
open BankAccountFormatter.Types.BankAccount.PublicTypes
open Xunit.Abstractions
open BankAccountFormatter.Interpreter.Interpreter
open FluentAssertions
open Xunit

module InterpreterTests =

    let testTeamData =
        {
            Prefix = 111
            AccountNumber = 10008888L
            BankCode = 200s
        }
    
    type InterpreterTests(output: ITestOutputHelper) =
        
        [<Fact>]
        member __.``Minimized format with separators returns correct result``() =
            let minimizedFormat = BankAccountFormatParts(
                [BankAccountPart(Prefix(MinimizedPrefix));
                 BankAccountSeparator(PrefixSeparator);
                 BankAccountPart(AccountNumber(MinimizedAccountNumber))
                 BankAccountSeparator(BankCodeSeparator)
                 BankAccountPart(BankCode(MinimizedBankCode))])

            let formattingFunc = interpret minimizedFormat
            let result = formattingFunc testTeamData
            result.Should().Be("111-10008888/200", null);

        [<Fact>]
        member __.``Minimized format without separators returns correct result``() =
            let minimizedFormat = BankAccountFormatParts(
                [BankAccountPart(Prefix(MinimizedPrefix));
                 BankAccountPart(AccountNumber(MinimizedAccountNumber))
                 BankAccountPart(BankCode(MinimizedBankCode))])

            let formattingFunc = interpret minimizedFormat
            let result = formattingFunc testTeamData
            result.Should().Be("11110008888200", null);

        [<Fact>]
        member __.``Padded format with separators returns correct result``() =
            let minimizedFormat = BankAccountFormatParts(
                [BankAccountPart(Prefix(PaddedPrefix));
                 BankAccountSeparator(PrefixSeparator);
                 BankAccountPart(AccountNumber(PaddedAccountNumber))
                 BankAccountSeparator(BankCodeSeparator)
                 BankAccountPart(BankCode(PaddedBankCode))])

            let formattingFunc = interpret minimizedFormat
            let result = formattingFunc testTeamData
            result.Should().Be("000111-0010008888/0200", null);

        [<Fact>]
        member __.``Padded format without separators returns correct result``() =
            let minimizedFormat = BankAccountFormatParts(
                [BankAccountPart(Prefix(PaddedPrefix));
                 BankAccountPart(AccountNumber(PaddedAccountNumber))
                 BankAccountPart(BankCode(PaddedBankCode))])

            let formattingFunc = interpret minimizedFormat
            let result = formattingFunc testTeamData
            result.Should().Be("00011100100088880200", null);

        [<Fact>]
        member __.``Format with other chars returns correct result``() =
            let minimizedFormat = BankAccountFormatParts(
                [BankAccountPart(Prefix(PaddedPrefix));
                 OtherChar ((char)' ');
                 OtherChar ((char)' ');
                 OtherChar ((char)' ');
                 OtherChar ((char)'%');
                 OtherChar ((char)'z');])

            let formattingFunc = interpret minimizedFormat
            let result = formattingFunc testTeamData
            result.Should().Be("000111   %z", null);