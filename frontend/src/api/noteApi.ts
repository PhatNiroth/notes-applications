import { http } from './http'
import type { Note, CreateNote, UpdateNote } from '@/types/note'

export const noteApi = {
  getAll: () => http.get<Note[]>('/notes'),
  getById: (id: number) => http.get<Note>(`/notes/${id}`),
  create: (dto: CreateNote) => http.post<Note>('/notes', dto),
  update: (id: number, dto: UpdateNote) => http.put<Note>(`/notes/${id}`, dto),
  delete: (id: number) => http.delete(`/notes/${id}`),
}
