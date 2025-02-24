import { ref } from 'vue'
import type { Stock, StockDailyInformation, StockExchange } from '../types/stock'
import { fetchStockExchanges, fetchStocksByDate, postNewStock } from '../services/StockService'

export function useStocks() {
  const stocksInformation = ref<StockDailyInformation[]>([])
  const exchanges = ref<StockExchange[]>([]);

  const loading = ref(false)
  const error = ref<string | null>(null)

  async function loadStocks(date: string) {
    loading.value = true
    error.value = null
    try {
      const data = await fetchStocksByDate(date)
      stocksInformation.value = data.map((stock: any) => ({
        openPrice: stock.open,
        closePrice: stock.close,
        highPrice: stock.high,
        lowPrice: stock.low,
        volume: stock.volume,
        stock: stock.stock,
        date: stock.date
      }));

    } catch (err) {
      error.value = (err as Error).message
      console.error('Failed while fetching data:', err)
    } finally {
      loading.value = false
    }
  }

  const handleAddStock = async ({ symbol, stockExchange, selectedDate }: { symbol: string, stockExchange: string, selectedDate: string }) => {
    const newStock: Stock = {
      symbol,
      stockExchange
    };
    
    try {
      await postNewStock(newStock);
    } catch (error) {
      console.error('Failed to add new stock:', error);
    }

    const newStockDailyInfo: StockDailyInformation = {
      openPrice: 0,
      closePrice: 0,
      highPrice: 0,
      lowPrice: 0,
      volume: 0,
      stock: newStock,
      date: selectedDate
    };
    
    stocksInformation.value.push(newStockDailyInfo);
  };

  const loadExchanges = async () => {
    try {
      const data = await fetchStockExchanges();
      exchanges.value = data;
    }
    catch (error) {
      console.error('Failed to fetch stock exchanges:', error);
    }
  };

  return {
    stocksInformation: stocksInformation,
    loading,
    error,
    loadStocks,
    handleAddStock,
    exchanges,
    loadExchanges
  }
}
