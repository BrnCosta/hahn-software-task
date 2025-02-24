<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { Dialog, DialogPanel, DialogTitle, TransitionChild, TransitionRoot } from '@headlessui/vue';
import { StockExchange } from '../types/stock';

const props = defineProps<{
  isOpen: boolean;
  selectedDate: string;
  exchanges: StockExchange[];
}>();

const emit = defineEmits<{
  'close': [],
  'add-stock': [{ symbol: string, stockExchange: string, selectedDate: string }]
  'load-exchanges': []
}>();

const symbol = ref('');
const selectedExchange = ref<StockExchange | null>(null);

onMounted(() => {
  emit('load-exchanges');
  selectedExchange.value = props.exchanges[0] || null;
});

watch(
  () => props.exchanges,
  (newExchanges) => {
    if (newExchanges.length > 0) {
      const defaultExchange = newExchanges.find((exchange) => exchange.value === 'US');
      selectedExchange.value = defaultExchange || newExchanges[0];
    }
  },
  { immediate: true }
);

const handleSubmit = () => {
  if (selectedExchange.value) {
    emit('add-stock', {
      symbol: symbol.value,
      stockExchange: selectedExchange.value.value,
      selectedDate: props.selectedDate
    });
  }
  symbol.value = '';
  selectedExchange.value = props.exchanges[0] || null;
  emit('close');
};
</script>

<template>
  <TransitionRoot appear :show="props.isOpen" as="template">
    <Dialog as="div" class="relative z-10" @close="emit('close')">
      <TransitionChild
        as="template"
        enter="duration-300 ease-out"
        enter-from="opacity-0"
        enter-to="opacity-100"
        leave="duration-200 ease-in"
        leave-from="opacity-100"
        leave-to="opacity-0"
      >
        <div class="fixed inset-0 bg-black/25 backdrop-blur-sm" />
      </TransitionChild>

      <div class="fixed inset-0 overflow-y-auto">
        <div class="flex min-h-full items-center justify-center p-4 text-center">
          <TransitionChild
            as="template"
            enter="duration-300 ease-out"
            enter-from="opacity-0 scale-95"
            enter-to="opacity-100 scale-100"
            leave="duration-200 ease-in"
            leave-from="opacity-100 scale-100"
            leave-to="opacity-0 scale-95"
          >
            <DialogPanel class="w-full max-w-md transform overflow-hidden rounded-2xl bg-white p-6 text-left align-middle shadow-xl transition-all">
              <DialogTitle as="h3" class="text-lg font-semibold leading-6 text-gray-900">
                Add New Stock
              </DialogTitle>

              <form @submit.prevent="handleSubmit" class="mt-6">
                <div class="space-y-4">
                  <div>
                    <label for="symbol" class="block text-sm font-medium text-gray-700 mb-2">Stock Symbol</label>
                    <input
                      type="text"
                      id="symbol"
                      v-model="symbol"
                      required
                      class="mt-1 block w-full rounded-xl bg-white border-gray-200 shadow-sm focus:border-blue-500 focus:ring-blue-500 text-base py-3 px-4 text-gray-900"
                      placeholder="Enter stock symbol"
                    />
                  </div>

                  <div>
                    <label for="exchange" class="block text-sm font-medium text-gray-700 mb-2">Exchange</label>
                    <select
                      id="exchange"
                      v-model="selectedExchange"
                      class="mt-1 block w-full rounded-xl bg-white border-gray-200 shadow-sm focus:border-blue-500 focus:ring-blue-500 text-base py-3 px-4 text-gray-900"
                    >
                    <option v-for="exchange in exchanges" :key="exchange.value" :value="exchange">
                      {{ exchange.description }} ({{ exchange.value }})
                    </option>
                    </select>
                  </div>
                </div>

                <div class="mt-8 flex justify-end space-x-3">
                  <button
                    type="button"
                    class="inline-flex justify-center rounded-xl border border-gray-200 bg-white px-6 py-3 text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                    @click="emit('close')"
                  >
                    Cancel
                  </button>
                  <button
                    type="submit"
                    class="inline-flex justify-center rounded-xl border border-transparent bg-blue-600 px-6 py-3 text-base font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                  >
                    Add Stock
                  </button>
                </div>
              </form>
            </DialogPanel>
          </TransitionChild>
        </div>
      </div>
    </Dialog>
  </TransitionRoot>
</template>