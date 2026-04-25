<script setup lang="ts">
import type { Note } from '../types'
import { formatDate } from '@/utils/date'

defineProps<{ note: Note }>()
defineEmits<{ edit: [note: Note]; delete: [id: number]; view: [note: Note] }>()
</script>

<template>
  <div @click="$emit('view', note)" class="bg-white rounded-[5px] shadow-sm border border-gray-100 p-5 flex flex-col gap-3 hover:shadow-md transition cursor-pointer">
    <div class="flex items-start justify-between gap-2">
      <h3 class="text-base font-semibold text-gray-800 line-clamp-2">{{ note.title }}</h3>
      <div class="flex gap-1 shrink-0">
        <button @click.stop="$emit('edit', note)"
          class="p-1.5 rounded-[5px] text-gray-400 hover:text-indigo-600 hover:bg-indigo-50 transition" title="Edit">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M15.232 5.232l3.536 3.536M9 13l6.5-6.5a2 2 0 012.828 2.828L11.828 15.828a4 4 0 01-1.414.94l-3 1 1-3a4 4 0 01.94-1.414z" />
          </svg>
        </button>
        <button @click.stop="$emit('delete', note.id)"
          class="p-1.5 rounded-[5px] text-gray-400 hover:text-red-500 hover:bg-red-50 transition" title="Delete">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6M9 7h6m2 0a1 1 0 00-1-1h-4a1 1 0 00-1 1m-4 0h10" />
          </svg>
        </button>
      </div>
    </div>

    <p v-if="note.content" class="text-sm text-gray-500 line-clamp-3">{{ note.content }}</p>

    <p class="text-xs text-gray-400 mt-auto">Updated {{ formatDate(note.updatedAt) }}</p>
  </div>
</template>
