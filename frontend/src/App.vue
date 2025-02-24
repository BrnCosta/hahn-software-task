<script setup lang="ts">
import { onMounted, ref } from 'vue';
import StockTable from './components/StockTable.vue';
import AddStockModal from './components/AddStockModal.vue';
import { ArrowPathIcon } from '@heroicons/vue/24/outline';

import { useStocks } from './composables/useStock';
import { StockDailyInformation } from './types/stock';

const searchQuery = ref('');
const isModalOpen = ref(false);
const selectedDate = ref(new Date().toISOString().split('T')[0]);

const { stocksInformation, loadStocks, handleAddStock, exchanges, loadExchanges } = useStocks()

const refreshData = () => {
  const recentlyAddedStocks: StockDailyInformation[] = stocksInformation.value.filter((stockInfo: StockDailyInformation) => {
    return stockInfo.volume === 0;
  });

  const updateStocks = () => {
    recentlyAddedStocks.forEach(stock => {
      const existingStock = stocksInformation.value.find(s => s.stock.symbol === stock.stock.symbol);
      if (!existingStock) {
        stocksInformation.value.push(stock);
      }
    });
  };

  // Avoid deleting the stocks that were added and have not been updated yet
  loadStocks(selectedDate.value).then(() => {
    loadStocks(selectedDate.value).then(updateStocks);
  });
};

onMounted(() => {
  loadStocks(selectedDate.value)
})

</script>

<template>
  <div class="min-h-screen bg-gray-50 py-8">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div class="mb-8">
        <h1 class="text-3xl font-bold text-gray-900">Stock Price Management</h1>
      </div>

      <div class="bg-white rounded-2xl shadow-lg p-6">
        <div class="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-6">
          <div class="flex-1 w-full sm:max-w-sm">
            <div class="relative">
              <input
                type="text"
                v-model="searchQuery"
                placeholder="Search stocks..."
                class="block w-full rounded-xl border bg-white border-gray-200 shadow-sm pl-4 pr-10 py-3 text-sm text-gray-900 focus:border-blue-500 focus:ring-blue-500"
              />
              <span class="absolute inset-y-0 right-0 flex items-center pr-3">
                <svg class="h-5 w-5 text-gray-400" viewBox="0 0 20 20" fill="currentColor">
                  <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
                </svg>
              </span>
            </div>
          </div>
          <div class="flex gap-3 w-full sm:w-auto">
            <input
              type="date"
              v-model="selectedDate"
              class="block w-full rounded-xl bg-white border border-gray-200 shadow-sm px-4 py-2 text-sm text-gray-900 focus:border-blue-500 focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50"
            />
            <button
              @click="refreshData"
              class="flex items-center justify-center px-4 py-2.5 border border-gray-200 text-sm font-medium rounded-xl text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 shadow-sm"
            >
              <ArrowPathIcon class="h-5 w-5 mr-2" />
              Refresh
            </button>
            <button
              @click="isModalOpen = true"
              class="flex-1 sm:flex-none px-6 py-2.5 text-sm font-medium rounded-xl text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 shadow-sm"
            >
              Add Stock
            </button>
          </div>
        </div>

        <StockTable
          :stocksInformation="stocksInformation"
          :searchQuery="searchQuery"
        />
      </div>
    </div>

    <AddStockModal
      :is-open="isModalOpen"
      :selected-date="selectedDate"
      @close="isModalOpen = false"
      @add-stock="handleAddStock"
      @load-exchanges="loadExchanges"
      :exchanges="exchanges"
    />
  </div>
</template>

<style>
@tailwind base;
@tailwind components;
@tailwind utilities;
</style>