<script setup lang="ts">
import type { Note } from '../types'
import { formatDateTime as formatDate } from '@/utils/date'

defineProps<{ note: Note | null; open: boolean }>()
defineEmits<{ close: []; edit: [note: Note] }>()
</script>

<template>
  <Teleport to="body">
    <div
      v-if="open && note"
      class="fixed inset-0 bg-black/40 z-50 flex items-center justify-center px-4 py-6"
      @click.self="$emit('close')"
    >
      <div class="bg-white rounded-[5px] shadow-xl w-full max-w-lg max-h-[90vh] flex flex-col">
        <div class="flex items-start justify-between gap-4 px-6 pt-6 pb-3 border-b border-gray-100">
          <h2 class="text-xl font-bold text-gray-800 break-words">{{ note.title }}</h2>
          <button @click="$emit('close')" class="text-gray-400 hover:text-gray-600 text-2xl leading-none shrink-0">&times;</button>
        </div>

        <div class="flex-1 overflow-y-auto px-6 py-4 space-y-4">
          <p class="text-gray-600 text-sm whitespace-pre-wrap break-words">
            {{ note.content || 'No content' }}
          </p>

          <div class="text-xs text-gray-400 space-y-1 border-t pt-3">
            <p>Created: {{ formatDate(note.createdAt) }}</p>
            <p>Updated: {{ formatDate(note.updatedAt) }}</p>
          </div>
        </div>

        <div class="flex justify-end gap-3 px-6 py-4 border-t border-gray-100 bg-gray-50 rounded-b-[5px]">
          <button
            @click="$emit('close')"
            class="px-4 py-2 text-sm rounded-[5px] border border-gray-300 text-gray-600 hover:bg-gray-50 bg-white transition"
          >
            Close
          </button>
          <button
            @click="$emit('edit', note)"
            class="px-4 py-2 text-sm rounded-[5px] bg-indigo-600 text-white font-semibold hover:bg-indigo-700 transition"
          >
            Edit
          </button>
        </div>
      </div>
    </div>
  </Teleport>
</template>
