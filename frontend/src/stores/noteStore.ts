import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { noteApi } from '@/api/noteApi'
import type { Note, CreateNote, UpdateNote } from '@/types/note'

export const useNoteStore = defineStore('notes', () => {
  const notes = ref<Note[]>([])
  const loading = ref(false)
  const searchQuery = ref('')
  const sortBy = ref<'createdAt' | 'title'>('createdAt')
  const sortOrder = ref<'asc' | 'desc'>('desc')

  const filteredNotes = computed(() => {
    let result = [...notes.value]

    if (searchQuery.value) {
      result = result.filter(n =>
        n.title.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        n.content?.toLowerCase().includes(searchQuery.value.toLowerCase())
      )
    }

    result.sort((a, b) => {
      const valA = sortBy.value === 'title' ? a.title : a.createdAt
      const valB = sortBy.value === 'title' ? b.title : b.createdAt
      return sortOrder.value === 'asc'
        ? valA.localeCompare(valB)
        : valB.localeCompare(valA)
    })

    return result
  })

  async function fetchNotes() {
    loading.value = true
    const { data } = await noteApi.getAll()
    notes.value = data
    loading.value = false
  }

  async function createNote(dto: CreateNote) {
    const { data } = await noteApi.create(dto)
    notes.value.unshift(data)
  }

  async function updateNote(id: number, dto: UpdateNote) {
    const { data } = await noteApi.update(id, dto)
    const index = notes.value.findIndex(n => n.id === id)
    if (index !== -1) notes.value[index] = data
  }

  async function deleteNote(id: number) {
    await noteApi.delete(id)
    notes.value = notes.value.filter(n => n.id !== id)
  }

  return {
    notes,
    loading,
    searchQuery,
    sortBy,
    sortOrder,
    filteredNotes,
    fetchNotes,
    createNote,
    updateNote,
    deleteNote,
  }
})
