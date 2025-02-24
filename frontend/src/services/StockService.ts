import type { Stock } from "../types/stock"

export async function fetchStocksByDate(date: string) {
  const response = await fetch(`http://localhost:5099/api/StockDailyInformation?date=${date}`)
  if (!response.ok) {
    throw new Error('Failed to fetch data from server.')
  }
  return response.json()
}

export async function postNewStock(stock: Stock) {
  const response = await fetch('http://localhost:5099/api/Stock', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(stock)
  })
  if (!response.ok) {
    throw new Error('Failed to add new stock.')
  }
}

export async function fetchStockExchanges() {
  const response = await fetch('http://localhost:5099/api/Stock/exchanges')
  if (!response.ok) {
    throw new Error('Failed to fetch data from server.')
  }
  return response.json()
}
