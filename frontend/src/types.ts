export interface CommentItem {
  id: number
  authorName: string
  message: string
  createdAtUtc: string
}

export interface PostItem {
  id: number
  title: string
  authorName: string
  imageUrl: string
  createdAtUtc: string
  comments: CommentItem[]
}
