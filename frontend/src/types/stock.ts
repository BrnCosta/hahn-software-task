export interface Stock {
  symbol: string;
  stockExchange: string;
}

export interface StockDailyInformation {
  openPrice: number;
  closePrice: number;
  highPrice: number;
  lowPrice: number;
  volume: number;
  stock: Stock;
  date: string;
}

export interface StockExchange {
  value: string;
  description: string;
}