<script setup lang="ts">
import { computed } from 'vue';
import type { StockDailyInformation } from '../types/stock';

const props = defineProps<{
  stocksInformation: StockDailyInformation[];
  searchQuery: string;
}>();

const filteredStocks = computed(() => {
  return props.stocksInformation.filter(stockInfo => 
    stockInfo.stock.symbol.toLowerCase().includes(props.searchQuery.toLowerCase()) ||
    stockInfo.stock.stockExchange.toLowerCase().includes(props.searchQuery.toLowerCase())
  );
});
</script>

<template>
  <div class="overflow-hidden rounded-xl border border-gray-200">
    <div class="overflow-x-auto">
      <table class="min-w-full divide-y divide-gray-200">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Symbol</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Open Price</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Close Price</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Highest Price</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Lowest Price</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Volume</th>
            <th scope="col" class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Exchange</th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr v-for="stockInfo in filteredStocks" :key="stockInfo.stock.symbol" class="transition-colors hover:bg-blue-50">
            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-blue-600">{{ stockInfo.stock.symbol }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">${{ stockInfo.openPrice.toFixed(2) }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">${{ stockInfo.closePrice.toFixed(2) }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">${{ stockInfo.highPrice.toFixed(2) }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">${{ stockInfo.lowPrice.toFixed(2) }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">{{ stockInfo.volume }}</td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600">{{ stockInfo.stock.stockExchange }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <p class="text-xs pt-2 place-self-end text-gray-500 custom-text-stroke">Information is updated automatically every hour.</p>
</template>