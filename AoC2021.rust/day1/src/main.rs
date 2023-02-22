fn main() {
    let input = include_str!("../input.txt")
        .lines()
        .map(|n| n.parse().unwrap())
        .collect::<Vec<u16>>();

    for l in input {
        println!("{}", l);
    }
}
