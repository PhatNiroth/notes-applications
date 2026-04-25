<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import NoteCard from "../components/NoteCard.vue";
import NoteModal from "../components/NoteModal.vue";
import NoteDetail from "../components/NoteDetail.vue";
import { useNoteStore } from "@/stores/noteStore";
import { useAuthStore } from "@/stores/authStore";
import type { Note } from "@/types";

const store = useNoteStore();
const auth = useAuthStore();
const router = useRouter();
const modalOpen = ref(false);

const logout = () => {
  auth.logout();
  router.push('/login');
};
const editingNote = ref<Note | null>(null);
const detailOpen = ref(false);
const viewingNote = ref<Note | null>(null);

const openDetail = (note: Note) => {
  viewingNote.value = note;
  detailOpen.value = true;
};

const closeDetail = () => {
  detailOpen.value = false;
  viewingNote.value = null;
};

onMounted(() => store.fetchNotes());

const openNewNote = () => {
  editingNote.value = null;
  modalOpen.value = true;
};

const openEdit = (note: Note) => {
  editingNote.value = note;
  modalOpen.value = true;
};

const handleSave = async (title: string, content: string) => {
  if (editingNote.value) {
    await store.updateNote(editingNote.value.id, { title, content });
  } else {
    await store.createNote({ title, content });
  }
  modalOpen.value = false;
};

const handleDelete = async (id: number) => {
  if (confirm("Are you sure you want to delete this note?")) {
    await store.deleteNote(id);
  }
};
</script>

<template>
  <div class="min-h-screen bg-gray-50">
    <header class="bg-white border-b border-gray-200 sticky top-0 z-10">
      <div
        class="max-w-5xl mx-auto px-4 py-3 flex items-center justify-between gap-4"
      >
        <h1 class="text-xl font-bold text-indigo-600">Notes</h1>
        <div class="flex items-center gap-3">
          <span class="text-sm text-gray-500 hidden sm:block">{{ auth.username }}</span>
          <button
            @click="logout"
            class="text-sm text-gray-500 hover:text-red-500 transition border border-gray-200 rounded-[5px] px-3 py-1.5"
          >
            Logout
          </button>
        </div>
      </div>
    </header>

    <main class="max-w-5xl mx-auto px-4 py-6 space-y-5">
      <div class="flex flex-col sm:flex-row gap-3">
        <div class="relative flex-1">
          <svg
            class="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-gray-400"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M21 21l-4.35-4.35M17 11A6 6 0 115 11a6 6 0 0112 0z"
            />
          </svg>
          <input
            v-model="store.searchQuery"
            type="text"
            placeholder="Search notes..."
            class="w-full pl-9 pr-9 py-2 text-sm border border-gray-300 rounded-[5px] focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
          <button
            v-if="store.searchQuery"
            @click="store.searchQuery = ''"
            type="button"
            class="absolute right-2 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 p-1"
            title="Clear search"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>
 
        <div class="flex gap-2 shrink-0">
          <button
            @click="store.sortBy = 'createdAt'"
            :class="store.sortBy === 'createdAt' ? 'bg-indigo-600 text-white border-indigo-600' : 'bg-white text-gray-600 border-gray-300 hover:bg-gray-50'"
            class="px-3 py-2 text-xs rounded-[5px] border transition font-medium"
          >
            Date
          </button>
          <button
            @click="store.sortBy = 'title'"
            :class="store.sortBy === 'title' ? 'bg-indigo-600 text-white border-indigo-600' : 'bg-white text-gray-600 border-gray-300 hover:bg-gray-50'"
            class="px-3 py-2 text-xs rounded-[5px] border transition font-medium"
          >
            Title
          </button>
          <button
            @click="store.sortOrder = store.sortOrder === 'asc' ? 'desc' : 'asc'"
            class="px-3 py-2 text-xs rounded-[5px] border transition font-medium bg-white text-gray-600 border-gray-300 hover:bg-gray-50"
          >
            {{ store.sortOrder === 'asc' ? '↑ Asc' : '↓ Desc' }}
          </button>
        </div>

        <button
          @click="openNewNote"
          class="bg-indigo-600 hover:bg-indigo-700 text-white text-sm font-semibold px-4 py-2 rounded-[5px] transition shrink-0"
        >
          + New Note
        </button>
      </div>

      <div v-if="store.loading" class="text-center text-gray-400 py-16">Loading notes...</div>

      <div v-else-if="store.filteredNotes.length === 0" class="text-center text-gray-400 py-16">
        No notes found. Create your first note!
      </div>

      <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        <NoteCard
          v-for="note in store.filteredNotes"
          :key="note.id"
          :note="note"
          @view="openDetail"
          @edit="openEdit"
          @delete="handleDelete"
        />
      </div>
    </main>
  </div>

  <NoteDetail
    :open="detailOpen"
    :note="viewingNote"
    @close="closeDetail"
    @edit="(note) => { closeDetail(); openEdit(note) }"
  />

  <NoteModal
    :open="modalOpen"
    :note="editingNote"
    @close="modalOpen = false"
    @save="handleSave"
  />

</template>
