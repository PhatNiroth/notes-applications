<script setup lang="ts">
import { ref, watch } from 'vue'
import type { Note } from '../types'

const props = defineProps<{
  note: Note | null
  open: boolean
}>()

const emit = defineEmits<{
  close: []
  save: [title: string, content: string]
}>()

const title = ref('')
const content = ref('')
const titleError = ref('')

watch(() => props.open, (val) => {
  if (val) {
    title.value = props.note?.title ?? ''
    content.value = props.note?.content ?? ''
    titleError.value = ''
  }
})

watch(title, (val) => {
  if (titleError.value && val.trim()) titleError.value = ''
})

function submit() {
  if (!title.value.trim()) {
    titleError.value = 'Please enter a title for your note'
    return
  }
  emit('save', title.value.trim(), content.value.trim())
}
</script>

<template>
  <Teleport to="body">
    <div v-if="open" class="fixed inset-0 bg-black/40 z-50 flex items-center justify-center px-4 py-6"
      @click.self="emit('close')">
      <div class="bg-white rounded-[5px] shadow-xl w-full max-w-lg max-h-[90vh] flex flex-col">
        <div class="px-6 pt-6 pb-3 border-b border-gray-100">
          <h2 class="text-lg font-semibold text-gray-800">
            {{ note ? 'Edit Note' : 'New Note' }}
          </h2>
        </div>

        <form @submit.prevent="submit" class="flex flex-col flex-1 overflow-hidden">
          <div class="flex-1 overflow-y-auto px-6 py-4 space-y-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Title <span class="text-red-500">*</span></label>
              <input v-model="title" type="text" maxlength="255" placeholder="Note title"
                :class="[
                  'w-full border rounded-[5px] px-4 py-2 text-sm focus:outline-none focus:ring-2',
                  titleError ? 'border-red-400 focus:ring-red-400' : 'border-gray-300 focus:ring-indigo-500'
                ]" />
              <p v-if="titleError" class="text-red-500 text-xs mt-1">{{ titleError }}</p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Content</label>
              <textarea v-model="content" rows="10" placeholder="Write your note here..."
                class="w-full border border-gray-300 rounded-[5px] px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 resize-y min-h-[150px]" />
            </div>
          </div>

          <div class="flex justify-end gap-3 px-6 py-4 border-t border-gray-100 bg-gray-50 rounded-b-[5px]">
            <button type="button" @click="emit('close')"
              class="px-4 py-2 text-sm rounded-[5px] border border-gray-300 text-gray-600 hover:bg-gray-50 bg-white transition">
              Cancel
            </button>
            <button type="submit"
              class="px-4 py-2 text-sm rounded-[5px] bg-indigo-600 text-white font-semibold hover:bg-indigo-700 transition">
              {{ note ? 'Save Changes' : 'Create Note' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </Teleport>
</template>
