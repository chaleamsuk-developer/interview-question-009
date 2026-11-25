import axios from 'axios'
import type { PostItem, CommentItem } from '../types'

const api = axios.create({ baseURL: import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5080/api' })

export async function getPost(id: number): Promise<PostItem> {
  const { data } = await api.get<PostItem>(`/posts/${id}`)
  return data
}

export async function createComment(postId: number, message: string): Promise<CommentItem> {
  const { data } = await api.post<CommentItem>(`/posts/${postId}/comments`, { message })
  return data
}
