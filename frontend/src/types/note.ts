export interface Note {
  id: number
  userId: number
  title: string
  content: string | null
  createdAt: string
  updatedAt: string
}

export interface CreateNote {
  title: string
  content?: string
}

export interface UpdateNote {
  title: string
  content?: string
}
