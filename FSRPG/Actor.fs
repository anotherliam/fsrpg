namespace FSRPG

type Actor = {
    Name: string;
    Team: int8;
}

module Actor =
    let doSomething actor =
        ()